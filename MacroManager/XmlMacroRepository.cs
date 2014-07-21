using MacroManager.Core;
using MacroManager.Core.Action;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MacroManager
{
    public class XmlMacroRepository : IMacroRepository
    {
        #region Constants

        private const string FILE_NAME = "Macros.xml";
        private const string ROOT_ELEMENT = "macros";
        private const string MACRO_LABEL = "macro";
        private const string MACRO_ACTION_LABEL = "action";
        private const string MACRO_ACTION_TYPE_LABEL = "type";
        private const string MACRO_ACTION_X_LABEL = "X";
        private const string MACRO_ACTION_Y_LABEL = "Y";
        private const string MACRO_ACTION_DURATION_LABEL = "duration";
        private const string LEFT_CLICK_ACTION_TYPE = "leftClickAction";
        private const string RIGHT_CLICK_ACTION_TYPE = "rightClickAction";
        private const string WAIT_ACTION_TYPE = "waitAction";
        private const string MACRO_ID_LABEL = "id";

        #endregion

        private readonly XDocument document;

        public XmlMacroRepository()
        {
            this.document = XDocument.Load(FILE_NAME);
        }

        public IEnumerable<Macro> Read()
        {
            return this.document
                .Element(ROOT_ELEMENT)
                .Elements(MACRO_LABEL)
                .Select(macro => new Macro(
                    macro.Elements(MACRO_ACTION_LABEL).Select(action =>
                    {
                        var type = action.Element(MACRO_ACTION_TYPE_LABEL).Value;
                        int x, y; // Variables are used in the cases where the type is a mouse action
                        switch (type)
                        {
                            case RIGHT_CLICK_ACTION_TYPE:
                                x = int.Parse(action.Element(MACRO_ACTION_X_LABEL).Value);
                                y = int.Parse(action.Element(MACRO_ACTION_Y_LABEL).Value);
                                return new RightClickAction(x, y) as UserAction;
                            case LEFT_CLICK_ACTION_TYPE:
                                x = int.Parse(action.Element(MACRO_ACTION_X_LABEL).Value);
                                y = int.Parse(action.Element(MACRO_ACTION_Y_LABEL).Value);
                                return new LeftClickAction(x, y) as UserAction;
                            case WAIT_ACTION_TYPE:
                                var duration = int.Parse(action.Element(MACRO_ACTION_DURATION_LABEL).Value);
                                return new WaitAction(duration);
                            default:
                                throw new Exception("Unknown action type!");
                        }
                    })
                        .ToList(),
                    Guid.Parse(macro.Attribute(MACRO_ID_LABEL).Value)
                ));
        }

        public void Add(Macro macro)
        {
            var root = document.Element(ROOT_ELEMENT);
            if (root.Elements(MACRO_LABEL).Any(x => x.Attribute(MACRO_ID_LABEL).Value == macro.macroId.ToString()))
            {
                throw new Exception("Can't add the same macro twice!");
            }
            var userActions = macro.GetUserActions();
            var macroXml = new XElement(
                MACRO_LABEL,
                new XAttribute(MACRO_ID_LABEL, macro.macroId.ToString()),
                userActions 
                    // Tempory solution, the last four actions are always the same:
                    //      1. Wait
                    //      2. Open MacroManager from task bar
                    //      3. Wait
                    //      4. Stop recording
                    .Take(userActions.Count() - 4)
                    .Select(action =>
                    {
                        var type = String.Empty;
                        if (action is LeftClickAction)
                        {
                            var tempAction = action as LeftClickAction;
                            type = LEFT_CLICK_ACTION_TYPE;
                            return new XElement(
                                MACRO_ACTION_LABEL,
                                new XElement(MACRO_ACTION_TYPE_LABEL, type),
                                new XElement(MACRO_ACTION_X_LABEL, tempAction.X),
                                new XElement(MACRO_ACTION_Y_LABEL, tempAction.Y)
                            );
                        }
                        else if (action is RightClickAction)
                        {
                            var tempAction = action as RightClickAction;
                            type = RIGHT_CLICK_ACTION_TYPE;
                            return new XElement(
                                MACRO_ACTION_LABEL,
                                new XElement(MACRO_ACTION_TYPE_LABEL, type),
                                new XElement(MACRO_ACTION_X_LABEL, tempAction.X),
                                new XElement(MACRO_ACTION_Y_LABEL, tempAction.Y)
                            );
                        }
                        else if (action is WaitAction) {
                            type = WAIT_ACTION_TYPE;
                            return new XElement(
                                MACRO_ACTION_LABEL,
                                new XElement(MACRO_ACTION_TYPE_LABEL, type),
                                new XElement(MACRO_ACTION_DURATION_LABEL, (action as WaitAction).Duration)
                            );
                        }
                        else
                        {
                            throw new NotImplementedException("Type is not implemeted");
                        }
                    })
            );
            root.Add(macroXml);
            document.Save(FILE_NAME);
        }

        public void Remove(Macro macro)
        {
            var toRemove = document.Element(ROOT_ELEMENT)
                .Elements(MACRO_LABEL)
                .FirstOrDefault(x => x.Attribute(MACRO_ID_LABEL).Value == macro.macroId.ToString());

            if (toRemove == null)
            {
                throw new Exception("Soupplied macro cannot be deleted since it does not exists in the repository.");
            }
            toRemove.Remove();
            document.Save(FILE_NAME);
        }

    }
}
