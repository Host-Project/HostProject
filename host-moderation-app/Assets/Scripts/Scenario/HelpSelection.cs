using Host;
using Host.Network;
using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSelection : MonoBehaviour
{
    private HelpRPC _helpRPC;

    public CustomDropdown EnigmeDropdown;
    public CustomDropdown HelpsDropdown;

    public class HelpCommand
    {
        public string Text;
        public string CommandName;

        public string TextParameter;
        public int ActionIndex;
    }

    private List<string> Enigmes;
    private List<List<HelpCommand>> Helps;

    // Start is called before the first frame update
    void Start()
    {
        if(GlobalElements.Instance != null)
        {
            _helpRPC = GlobalElements.Instance.HelpRPC;
        }

        Enigmes = new List<string>();
        Enigmes.Add("Garrot");
        Enigmes.Add("Message crypt�");
        Enigmes.Add("Monitoring");
        Enigmes.Add("Seringues");
        Enigmes.Add("Calcul mental - Tableau p�riodique �l�ment");

        Helps = new List<List<HelpCommand>>();

        var garrot = new List<HelpCommand>();

        garrot.Add(new HelpCommand() { CommandName = "SendText", Text = "Code non s�curis�", TextParameter = "Le code n'est pas s�curis�." });
        garrot.Add(new HelpCommand() { CommandName = "SendText", Text = "Num�ro de la salle", TextParameter = "Le num�ro de la salle a son importance" });
        garrot.Add(new HelpCommand() { CommandName = "SendText", Text = "Garrot sur le patient", TextParameter = "Amener les bandages au patient !" });
        

        Helps.Add(garrot);

        var cryptedMessage = new List<HelpCommand>();
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendText", Text = "Toquer � la porte", TextParameter = "Quelqu'un a toqu� � la porte. Regardez dans les alentours." });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendImage", Text = "Image tablette re�ue", ActionIndex = 54 });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Fl�che tablette", ActionIndex = 55 });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendImage", Text = "Exemple de d�chiffrage", ActionIndex = 56 });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Fl�ches sur toutes les cl�es", ActionIndex = 57 });

        Helps.Add(cryptedMessage);


        var monitoring = new List<HelpCommand>();
        monitoring.Add(new HelpCommand() { CommandName = "SendText", Text = "Rester vers le patient", TextParameter = "Restez vers le patient pour s'assurer que celui-ci va bien." });
        monitoring.Add(new HelpCommand() { CommandName = "SendText", Text = "Interagir avec le monitoring", TextParameter = "Int�ragissez avec la machine de monitoring pour garantir l'�tat du patient." });
        monitoring.Add(new HelpCommand() { CommandName = "SendText", Text = "Ne vous arr�tez pas", TextParameter = "Ne vous arr�tez pas d'interagir avec la machine." });
        monitoring.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Fl�ches monitoring", ActionIndex = 58 });

        Helps.Add(monitoring);


        var seringues = new List<HelpCommand>();
        seringues.Add(new HelpCommand() { CommandName = "SendText", Text = "Attention aux seringues", TextParameter = "Inspectez bien les seringues sous tous les angles." });
        seringues.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Fl�che seringue", ActionIndex = 59 });
        seringues.Add(new HelpCommand() { CommandName = "SendText", Text = "Texte des seringues", TextParameter = "Le texte sur les seringues forment un mot." });

        Helps.Add(seringues);

        var labyrinthes = new List<HelpCommand>();
        labyrinthes.Add(new HelpCommand() { CommandName = "SendText", Text = "Labyrinthes pas l� par hasard", TextParameter = "Les labyrinthes ne sont pas l� par hasard." });
        labyrinthes.Add(new HelpCommand() { CommandName = "SendText", Text = "Dessiner sur une feuille", TextParameter = "Utiliez les feuilles et stylo � disposition." });
        labyrinthes.Add(new HelpCommand() { CommandName = "SendText", Text = "Dessiner la r�solution du labyrinthe", TextParameter = "N'h�sitez pas � dessiner la r�solution du labyrinthe." });
        labyrinthes.Add(new HelpCommand() { CommandName = "SendImage", Text = "Exemple de r�solution", ActionIndex = 60 });

        Helps.Add(labyrinthes);

        var periodicTable = new List<HelpCommand>();
        periodicTable.Add(new HelpCommand() { CommandName = "SendText", Text = "Calcul mental", TextParameter = "Un peu de calcul mental." });
        periodicTable.Add(new HelpCommand() { CommandName = "SendText", Text = "Correspondance avec un chiffre", TextParameter = "Les �l�ments ont probablement une correspondance avec un nombre." });
        periodicTable.Add(new HelpCommand() { CommandName = "SendText", Text = "Monitoring indice", TextParameter = "Peut �tre que la personne au monitoring a un indice..." });
        periodicTable.Add(new HelpCommand() { CommandName = "SendImage", Text = "Tableau periodique", ActionIndex = 61 });

        Helps.Add(periodicTable);

        var numbersGrid = new List<HelpCommand>();
        numbersGrid.Add(new HelpCommand() { CommandName = "SendText", Text = "Fl�ches utiles", TextParameter = "Cherchez des fl�ches dans la salle." });
        numbersGrid.Add(new HelpCommand() { CommandName = "SendText", Text = "Point de d�part", TextParameter = "Il faut d�marrer sur le rond" });
        numbersGrid.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Ou sont les fl�ches", ActionIndex=62 });
        numbersGrid.Add(new HelpCommand() { CommandName = "SendImage", Text = "Suite de fl�ches", ActionIndex = 63 });

        Helps.Add(numbersGrid);

        EnigmeDropdown.dropdownItems.Clear();



        foreach(var item in Enigmes)
        {
            EnigmeDropdown.SetItemTitle(item);
            EnigmeDropdown.CreateNewItem();
        }

        EnigmeDropdown.ChangeDropdownInfo(0);

        // Setup with folder by default

        HelpsDropdown.dropdownItems.Clear();

        foreach (var item in garrot)
        {
            HelpsDropdown.SetItemTitle(item.Text);
            HelpsDropdown.CreateNewItem();
        }

        HelpsDropdown.ChangeDropdownInfo(0);
    }

    public void ChangeSelectedEnigme(int index)
    {
        if (index >= Helps.Count)
            return;

        HelpsDropdown.index = 0;
        HelpsDropdown.selectedItemIndex = 0;
        HelpsDropdown.dropdownItems.Clear();

        foreach (var item in Helps[index])
        {
            HelpsDropdown.SetItemTitle(item.Text);
            HelpsDropdown.CreateNewItem();
        }

        HelpsDropdown.ChangeDropdownInfo(0);
    }

    public void ChangeSelectedHelp(int index)
    {

    }

    public void SendButtonSelected()
    {
        InvokeCurrentHelpMessage();
    }

    public void InvokeCurrentHelpMessage()
    {

        int index = EnigmeDropdown.selectedItemIndex;
        int helpIndex = HelpsDropdown.selectedItemIndex;

        HelpCommand command = Helps[index][helpIndex];

        Debug.Log($"Invoking an help message: {command.CommandName}, {command.Text}");

        _currentCommand = command;

        Invoke(command.CommandName, 0f);
    }

    private HelpCommand _currentCommand;

    public void SendText()
    {
        MessageEvent m = new MessageEvent(scenario: 0, type: "Alert", content: _currentCommand.TextParameter, recipient: "All");  ;
        _helpRPC.SendEvent(m);
    }

    public void SendImage()
    {
        HelpEvent m = new HelpEvent(scenario: 0, action_number: _currentCommand.ActionIndex.ToString(), recipient: "All", name: _currentCommand.Text);
        _helpRPC.SendEvent(m);
    }

    public void SendEvent()
    {
        HelpEvent m = new HelpEvent(scenario: 0, action_number: _currentCommand.ActionIndex.ToString(), recipient: "All", name: _currentCommand.Text);
        _helpRPC.SendEvent(m);
    }

}
