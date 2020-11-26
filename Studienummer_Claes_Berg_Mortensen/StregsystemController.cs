using Studienummer_Claes_Berg_Mortensen.Core;
using Studienummer_Claes_Berg_Mortensen.Events;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using Studienummer_Claes_Berg_Mortensen.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Studienummer_Claes_Berg_Mortensen
{
    public delegate void CommandEntered(CommandEnteredArgs e);
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
            _adminCommands.Add("crediton", (List<string> args) => HandlePurchaseableOnCredit(args)); //method for handling product able to purchased on credit
            _adminCommands.Add(":creditoff", (List<string> args) => HandleNotPurchaseableOnCredit(args));
            _adminCommands.Add(":addcredits", (List<string> args) => HandleAddCreditToUser(args)); //method for handling adding credits
        }

        public void ParseCommand(string command)
        {
            if (string.IsNullOrEmpty(command) == false)
            {
                command = command.ToLower();
                List<string> commandInputs = command.Split(new char[] { ' ' }).ToList();
                {
                    if (_adminCommands.ContainsKey(commandInputs[0]))
                        _adminCommands[commandInputs[0]].Invoke(commandInputs.GetRange(1, commandInputs.Count));
                    else
                        HandleUserInput(commandInputs);
                }
            }
        }
        public void HandleUserInput(List<string> commandInputs)
        {
            switch (commandInputs.Count)
            {
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
        }
        public void HandlePurchase(string username, int productID, int productAmount)
        {
            User user;
            Product product;

            user = _stregsystem.GetUserByUsername(username);
            product = _stregsystem.GetProductByID(productID);
            for(int i = 1; i <= productAmount; i++)
            {
                BuyTransaction userTransaction = _stregsystem.BuyProduct(user, product);
                _stregystemui.DisplayUserBuysProduct(userTransaction);
            }
        }   
        public void HandlePurchaseableOnCredit(List<string> productID)
        {
            HandlePurchaseableOrNotOnCredit(productID, true);
        }
        public void HandleNotPurchaseableOnCredit(List<string> productID)
        {
            HandlePurchaseableOrNotOnCredit(productID, false);
        }
        public void HandleActivateProduct(List<string> productID)
        {

            HandleActivateAndDeactivateProduct(productID, true);
        }
        public void HandleDeactivateProduct(List<string> productID)
        {
            HandleActivateAndDeactivateProduct(productID, false);
        }
        public void HandlePurchaseableOrNotOnCredit(List<string> productID,bool setPurchaseableOnCredit )
        {
            int id = 0;
            if (Regex.IsMatch(productID[0], @"^\d+$"))
                id = int.Parse(productID[0]);
            Product product = _stregsystem.GetProductByID(id);
            product.CanBeBoughtOnCredit = setPurchaseableOnCredit;
        }
        public void HandleActivateAndDeactivateProduct(List<string> productID, bool setActive)
        {
            int id = 0;
            if (Regex.IsMatch(productID[0], @"^\d+$"))
                id = int.Parse(productID[0]); 
            Product product = _stregsystem.GetProductByID(id);
            if (product.GetType != SeasonalProduct)
                product.Active = setActive;
        }
        public void HandleAddCreditToUser(List<string> args)
        {
            User user = _stregsystem.GetUserByUsername(args[1]);
            int amount = int.Parse(args[2]);
            _stregsystem.AddCreditsToAccount(user, amount);
        }
    }
}
