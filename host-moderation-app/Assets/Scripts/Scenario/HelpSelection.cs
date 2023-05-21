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
        Enigmes.Add("Message crypté");
        Enigmes.Add("Monitoring");
        Enigmes.Add("Seringues");
        Enigmes.Add("Calcul mental - Tableau périodique élément");

        Helps = new List<List<HelpCommand>>();

        var garrot = new List<HelpCommand>();

        garrot.Add(new HelpCommand() { CommandName = "SendText", Text = "Code non sécurisé", TextParameter = "Le code n'est pas sécurisé." });
        garrot.Add(new HelpCommand() { CommandName = "SendText", Text = "Numéro de la salle", TextParameter = "Le numéro de la salle a son importance" });
        garrot.Add(new HelpCommand() { CommandName = "SendText", Text = "Garrot sur le patient", TextParameter = "Amener les bandages au patient !" });
        

        Helps.Add(garrot);

        var cryptedMessage = new List<HelpCommand>();
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendText", Text = "Toquer à la porte", TextParameter = "Quelqu'un a toqué à la porte. Regardez dans les alentours." });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendImage", Text = "Image tablette reçue", ActionIndex = 54 });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Flèche tablette", ActionIndex = 55 });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendImage", Text = "Exemple de déchiffrage", ActionIndex = 56 });
        cryptedMessage.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Flèches sur toutes les clées", ActionIndex = 57 });

        Helps.Add(cryptedMessage);


        var monitoring = new List<HelpCommand>();
        monitoring.Add(new HelpCommand() { CommandName = "SendText", Text = "Rester vers le patient", TextParameter = "Restez vers le patient pour s'assurer que celui-ci va bien." });
        monitoring.Add(new HelpCommand() { CommandName = "SendText", Text = "Interagir avec le monitoring", TextParameter = "Intéragissez avec la machine de monitoring pour garantir l'état du patient." });
        monitoring.Add(new HelpCommand() { CommandName = "SendText", Text = "Ne vous arrêtez pas", TextParameter = "Ne vous arrêtez pas d'interagir avec la machine." });
        monitoring.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Flèches monitoring", ActionIndex = 58 });

        Helps.Add(monitoring);


        var seringues = new List<HelpCommand>();
        seringues.Add(new HelpCommand() { CommandName = "SendText", Text = "Attention aux seringues", TextParameter = "Inspectez bien les seringues sous tous les angles." });
        seringues.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Flèche seringue", ActionIndex = 59 });
        seringues.Add(new HelpCommand() { CommandName = "SendText", Text = "Texte des seringues", TextParameter = "Le texte sur les seringues forment un mot." });

        Helps.Add(seringues);

        var labyrinthes = new List<HelpCommand>();
        labyrinthes.Add(new HelpCommand() { CommandName = "SendText", Text = "Labyrinthes pas là par hasard", TextParameter = "Les labyrinthes ne sont pas là par hasard." });
        labyrinthes.Add(new HelpCommand() { CommandName = "SendText", Text = "Dessiner sur une feuille", TextParameter = "Utiliez les feuilles et stylo à disposition." });
        labyrinthes.Add(new HelpCommand() { CommandName = "SendText", Text = "Dessiner la résolution du labyrinthe", TextParameter = "N'hésitez pas à dessiner la résolution du labyrinthe." });
        labyrinthes.Add(new HelpCommand() { CommandName = "SendImage", Text = "Exemple de résolution", ActionIndex = 60 });

        Helps.Add(labyrinthes);

        var periodicTable = new List<HelpCommand>();
        periodicTable.Add(new HelpCommand() { CommandName = "SendText", Text = "Calcul mental", TextParameter = "Un peu de calcul mental." });
        periodicTable.Add(new HelpCommand() { CommandName = "SendText", Text = "Correspondance avec un chiffre", TextParameter = "Les éléments ont probablement une correspondance avec un nombre." });
        periodicTable.Add(new HelpCommand() { CommandName = "SendText", Text = "Monitoring indice", TextParameter = "Peut être que la personne au monitoring a un indice..." });
        periodicTable.Add(new HelpCommand() { CommandName = "SendImage", Text = "Tableau periodique", ActionIndex = 61 });

        Helps.Add(periodicTable);

        var numbersGrid = new List<HelpCommand>();
        numbersGrid.Add(new HelpCommand() { CommandName = "SendText", Text = "Flèches utiles", TextParameter = "Cherchez des flèches dans la salle." });
        numbersGrid.Add(new HelpCommand() { CommandName = "SendText", Text = "Point de départ", TextParameter = "Il faut démarrer sur le rond" });
        numbersGrid.Add(new HelpCommand() { CommandName = "SendEvent", Text = "Ou sont les flèches", ActionIndex=62 });
        numbersGrid.Add(new HelpCommand() { CommandName = "SendImage", Text = "Suite de flèches", ActionIndex = 63 });

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
