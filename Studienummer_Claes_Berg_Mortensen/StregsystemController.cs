using Studienummer_Claes_Berg_Mortensen.Core;
using Studienummer_Claes_Berg_Mortensen.CustomExceptions;
using Studienummer_Claes_Berg_Mortensen.Events;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using Studienummer_Claes_Berg_Mortensen.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Studienummer_Claes_Berg_Mortensen
{
    public class StregsystemController
    {
        IStregsystem _stregsystem;
        IStregsystemUI _stregystemui;

        Dictionary<string, Action<List<string>>> _adminCommands = new Dictionary<string, Action<List<string>>>();

        public StregsystemController(IStregsystemUI stregsystemCLI, IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            _stregystemui = stregsystemCLI;
            _stregystemui.CommandEntered += ParseCommand;

            _adminCommands.Add(":q", (List<string> args) => stregsystemCLI.Close());
            _adminCommands.Add(":quit", (List<string> args) => stregsystemCLI.Close());
            _adminCommands.Add(":activate", (List<string> args) => HandleActivateProduct(args));  //method for handling activation
            _adminCommands.Add(":deactivate", (List<string> args) => HandleDeactivateProduct(args));
            _adminCommands.Add(":crediton", (List<string> args) => HandlePurchaseableOnCredit(args)); //method for handling product able to purchased on credit
            _adminCommands.Add(":creditoff", (List<string> args) => HandleNotPurchaseableOnCredit(args));
            _adminCommands.Add(":addcredits", (List<string> args) => HandleAddCreditToUser(args)); //method for handling adding credits
        }

        public void ParseCommand(CommandEnteredArgs e)
        {
            if (string.IsNullOrEmpty(e.Command) == false)
            {
               
                List<string> commandInputs = e.Command.ToLower().Split(new char[] { ' ' }).ToList();
                if (_adminCommands.ContainsKey(commandInputs[0]))
                    _adminCommands[commandInputs[0]].Invoke(commandInputs.GetRange(1, commandInputs.Count -1));
                else
                    HandleUserInput(commandInputs);
            }
        }
        public void HandleUserInput(List<string> commandInputs)
        {
            if((commandInputs.Count == 1)||(Regex.IsMatch(commandInputs[1], @"^[1-9 ]\d*$")))
            switch (commandInputs.Count)
            {
                case 1:
                    {
                        User user = null;
                        try
                        {
                            user = _stregsystem.GetUserByUsername(commandInputs[0]);
                        }
                        catch (UserNotFoundException)
                        {
                            _stregystemui.DisplayUserNotFound(commandInputs[0]);
                            return;
                        }
                        _stregystemui.DisplayUserInfo(user);
                        _stregystemui.DisplayPastTransactions(user);
                        break;
                    }
                case 2:
                    {
                        HandlePurchase(commandInputs[0], int.Parse(commandInputs[1]), 1);
                        break;
                    }
                case 3:
                    {
                        HandlePurchase(commandInputs[0], int.Parse(commandInputs[1]), int.Parse(commandInputs[2]));
                        break;
                    }
                default:
                    {
                        _stregystemui.DisplayGeneralError("Der skete en fejl, tjek om syntax er korrekt.");
                        break;
                    }
            }
            else
            {
                _stregystemui.DisplayuProductNotFound(commandInputs[1]);
                return;
            }
        }
        /// <summary>
        /// Handles a user purchase based on the given product ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="productID"></param>
        /// <param name="productAmount"></param>
        public void HandlePurchase(string username, int productID, int productAmount)
        {
            User user = null;
            Product product;
            try
            {
                user = _stregsystem.GetUserByUsername(username);
            }
            catch (UserNotFoundException)
            {
                _stregystemui.DisplayUserNotFound(username);
                return;
            }
            product = _stregsystem.GetProductByID(productID);
            
                BuyTransaction userTransaction = null;
                try
                {
                    userTransaction = _stregsystem.BuyProduct(user, product);
                }
                catch (ProductInactiveException)
                {
                    _stregystemui.DisplayuProductNotFound(Convert.ToString(product.ID));
                    return;
                }
                if (productAmount == 1)
                    _stregystemui.DisplayUserBuysProduct(userTransaction);
                else
                    _stregystemui.DisplayUserBuysProduct(productAmount, userTransaction);
            
        }   
        /// <summary>
        /// Sets the product to purchaseable on credit based on the product ID
        /// </summary>
        /// <param name="productID"></param>
        public void HandlePurchaseableOnCredit(List<string> productID)
        {
            HandlePurchaseableOrNotOnCredit(productID, true);
        }
        /// <summary>
        /// Removes the option to purchase on credit from a product based on product ID
        /// </summary>
        /// <param name="productID"></param>
        public void HandleNotPurchaseableOnCredit(List<string> productID)
        {
            HandlePurchaseableOrNotOnCredit(productID, false);
        }
        /// <summary>
        /// activates a product based on the product ID
        /// </summary>
        /// <param name="productID"></param>
        public void HandleActivateProduct(List<string> productID)
        {

            HandleActivateAndDeactivateProduct(productID, true);
        }
        /// <summary>
        /// Deactivates a product based on the product ID
        /// </summary>
        /// <param name="productID"></param>
        public void HandleDeactivateProduct(List<string> productID)
        {
            HandleActivateAndDeactivateProduct(productID, false);
        }
        /// <summary>
        /// Handles the logic of changing wether or not a product is purchaseable on credit
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="setPurchaseableOnCredit"></param>
        public void HandlePurchaseableOrNotOnCredit(List<string> productID,bool setPurchaseableOnCredit )
        {
            int id = 0;
            if (Regex.IsMatch(productID[0], @"^\d+$"))
                id = int.Parse(productID[0]);
            else
                return;
            Product product = _stregsystem.GetProductByID(id);
            product.CanBeBoughtOnCredit = setPurchaseableOnCredit;
        }
        /// <summary>
        /// Andles the logic behind activating and deactivating products based on ID
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="setActive"></param>
        public void HandleActivateAndDeactivateProduct(List<string> productID, bool setActive)
        {
            int id = 0;
            if (Regex.IsMatch(productID[0], @"^\d+$"))
                id = int.Parse(productID[0]);
            else
                return;
            Product product = _stregsystem.GetProductByID(id);
            //if (product.GetType != SeasonalProduct)
                product.Active = setActive;
        }
        /// <summary>
        /// Adds credits (stregdollars) to a user based on the username and amount of credit
        /// </summary>
        /// <param name="args"></param>
        public void HandleAddCreditToUser(List<string> args)
        {
            User user = _stregsystem.GetUserByUsername(args[0]);
            int amount = int.Parse(args[1]);
            _stregsystem.AddCreditsToAccount(user, amount);
        }
    }
}
