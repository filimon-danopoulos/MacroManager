
using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;

namespace MacroManager.Data
{
    /// <summary>
    /// Repository class for persisting Macros to an XML file. 
    /// </summary>
    public class XmlMacroRepository : IFileMacroRepository
    {
        #region Constants
        private const string MACRO_ROOT_LABEL = "macros";
        private const string MACRO_LABEL = "macro";
        private const string MACRO_ID_LABEL = "id";
        private const string MACRO_NAME_LABEL = "name";
        private const string MACRO_DESCRIPTION_LABEL = "description";

        private const string MACRO_ACTION_LABEL = "action";
        private const string MACRO_ACTION_TYPE_LABEL = "type";
        private const string MACRO_ACTION_X_LABEL = "X";
        private const string MACRO_ACTION_Y_LABEL = "Y";
        private const string MACRO_ACTION_DURATION_LABEL = "duration";
        private const string MACRO_ACTION_KEY_LABEL = "key";
        private const string MACRO_ACTION_BUTTON_LABEL = "button";
        private const string MACRO_ACTION_PATH_LABEL = "path";
        private const string MACRO_ACTION_POINT_LABEL = "point";

        private const string CLICK_ACTION_TYPE = "clickAction";
        private const string LONG_CLICK_ACTION_TYPE = "longClickAction";
        private const string WAIT_ACTION_TYPE = "waitAction";
        private const string KEY_PRESS_ACTION_TYPE = "keyPressAction";
        private const string DRAG_ACTION_TYPE = "dragAction";

        #endregion

        #region Fields

        // TODO: Figure out a better way to handle these constants. Reflection maybe?
        private string FILE_NAME;
        /// <summary>
        /// The XML document we are working with.
        /// </summary>
        private XDocument document;

        #endregion

        #region Constructors

        /// <summary>
        /// Inializes the document field from a file. 
        /// Creates an in memory document if the file does not exist.
        /// </summary>
        public XmlMacroRepository(string fileName) : this(fileName, false)
        {
        }

        /// <summary>
        /// Intializes the document field from a file. Provides a means to forcefully create a new
        /// file. This WILL remove any existing file if forceNew is true.
        /// </summary>
        public XmlMacroRepository(string fileName, bool forceNew)
        {
            FILE_NAME = fileName;
            var fileExists = File.Exists(FILE_NAME);
            if (!fileExists || forceNew)
            {
                if (fileExists)
                {
                    File.Delete(FILE_NAME);
                }
                this.document = new XDocument(
                    new XElement(MACRO_ROOT_LABEL, "")
                );
            }
            else
            {
                this.document = XDocument.Load(fileName);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns all the macros from the XML file.
        /// </summary>
        public IEnumerable<Macro> Read()
        {
            return this.document
                .Element(MACRO_ROOT_LABEL)
                .Elements(MACRO_LABEL)
                .Select(macro => new Macro(
                    macro.Elements(MACRO_ACTION_LABEL).Select(action =>
                    {
                        var type = action.Element(MACRO_ACTION_TYPE_LABEL).Value;
                        switch (type)
                        {
                            case CLICK_ACTION_TYPE:
                                return new ClickAction(
                                    int.Parse(action.Element(MACRO_ACTION_X_LABEL).Value),
                                    int.Parse(action.Element(MACRO_ACTION_Y_LABEL).Value),
                                    (ClickAction.MouseButton)int.Parse(action.Element(MACRO_ACTION_BUTTON_LABEL).Value)
                                ) as UserAction;
                            case LONG_CLICK_ACTION_TYPE:
                                return new LongClickAction(
                                    int.Parse(action.Element(MACRO_ACTION_X_LABEL).Value),
                                    int.Parse(action.Element(MACRO_ACTION_Y_LABEL).Value),
                                    (ClickAction.MouseButton)int.Parse(action.Element(MACRO_ACTION_BUTTON_LABEL).Value),
                                    int.Parse(action.Element(MACRO_ACTION_DURATION_LABEL).Value)
                                );
                            case DRAG_ACTION_TYPE:
                                return new DragAction(
                                    (ClickAction.MouseButton)int.Parse(action.Element(MACRO_ACTION_BUTTON_LABEL).Value),
                                    action.Element(MACRO_ACTION_PATH_LABEL)
                                        .Elements(MACRO_ACTION_POINT_LABEL)
                                        .Select(x => new Point(
                                            int.Parse(x.Element(MACRO_ACTION_X_LABEL).Value),
                                            int.Parse(x.Element(MACRO_ACTION_Y_LABEL).Value))
                                        )
                                );
                            case KEY_PRESS_ACTION_TYPE:
                                return new KeyboardAction(
                                    int.Parse(action.Element(MACRO_ACTION_KEY_LABEL).Value)
                                ) as UserAction;
                            case WAIT_ACTION_TYPE:
                                return new WaitAction(
                                    int.Parse(action.Element(MACRO_ACTION_DURATION_LABEL).Value)
                                );
                            default:
                                throw new Exception("Unknown action type!");
                        }
                    })
                    .ToList(),
                    Guid.Parse(macro.Attribute(MACRO_ID_LABEL).Value),
                    macro.Attribute(MACRO_NAME_LABEL).Value,
                    macro.Element(MACRO_DESCRIPTION_LABEL).Value
                ));
        }

        /// <summary>
        /// Adds the supplied Macro to the XML file and saves it.
        /// </summary>
        public void Add(Macro macro)
        {
            var root = document.Element(MACRO_ROOT_LABEL);
            if (root.Elements(MACRO_LABEL).Any(x => x.Attribute(MACRO_ID_LABEL).Value == macro.MacroId.ToString()))
            {
                throw new Exception("Can't add the same macro twice!");
            }
            var userActions = macro.GetUserActions();
            var macroXml = new XElement(
                MACRO_LABEL,
                new XAttribute(MACRO_ID_LABEL, macro.MacroId.ToString()),
                new XAttribute(MACRO_NAME_LABEL, macro.Name),
                new XElement(MACRO_DESCRIPTION_LABEL, macro.Description),
                userActions
                    .Select(action =>
                    {
                        if (action is DragAction)
                        {
                            var dragAction = action as DragAction;
                            return new XElement(
                                MACRO_ACTION_LABEL,
                                new XElement(MACRO_ACTION_TYPE_LABEL, DRAG_ACTION_TYPE),
                                new XElement(MACRO_ACTION_BUTTON_LABEL, (int)dragAction.PressedButton),
                                new XElement(MACRO_ACTION_PATH_LABEL, dragAction.Path
                                    .Select(x => new XElement(MACRO_ACTION_POINT_LABEL, new[] {
                                            new XElement(MACRO_ACTION_X_LABEL, x.X),
                                            new XElement(MACRO_ACTION_Y_LABEL, x.Y)
                                        }))
                                )
                            );
                        }
                        else if (action is ClickAction)
                        {

                            var longClickAction = action as LongClickAction;
                            if (longClickAction != null)
                            {
                                return new XElement(
                                    MACRO_ACTION_LABEL,
                                    new XElement(MACRO_ACTION_TYPE_LABEL, LONG_CLICK_ACTION_TYPE),
                                    new XElement(MACRO_ACTION_X_LABEL, longClickAction.X),
                                    new XElement(MACRO_ACTION_Y_LABEL, longClickAction.Y),
                                    new XElement(MACRO_ACTION_BUTTON_LABEL, (int)longClickAction.PressedButton),
                                    new XElement(MACRO_ACTION_DURATION_LABEL, longClickAction.Duration)
                                );
                            }
                            var clickAction = action as ClickAction;
                            return new XElement(
                                MACRO_ACTION_LABEL,
                                new XElement(MACRO_ACTION_TYPE_LABEL, CLICK_ACTION_TYPE),
                                new XElement(MACRO_ACTION_X_LABEL, clickAction.X),
                                new XElement(MACRO_ACTION_Y_LABEL, clickAction.Y),
                                new XElement(MACRO_ACTION_BUTTON_LABEL, (int)clickAction.PressedButton)
                            );
                        }
                        else if (action is KeyboardAction)
                        {
                            return new XElement(
                                MACRO_ACTION_LABEL,
                                new XElement(MACRO_ACTION_TYPE_LABEL, KEY_PRESS_ACTION_TYPE),
                                new XElement(MACRO_ACTION_KEY_LABEL, (action as KeyboardAction).VirtualKey)
                            );
                        }
                        else if (action is WaitAction)
                        {
                            return new XElement(
                                MACRO_ACTION_LABEL,
                                new XElement(MACRO_ACTION_TYPE_LABEL, WAIT_ACTION_TYPE),
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
        }

        /// <summary>
        /// Removes the supplied macro from the document and saves it.
        /// </summary>
        public void Remove(Macro macro)
        {
            var toRemove = document.Element(MACRO_ROOT_LABEL)
                .Elements(MACRO_LABEL)
                .FirstOrDefault(x => x.Attribute(MACRO_ID_LABEL).Value == macro.MacroId.ToString());

            if (toRemove == null)
            {
                throw new Exception("Supplied macro cannot be deleted since it does not exists in the repository.");
            }
            toRemove.Remove();
        }

        /// <summary>
        /// Saves all the changes made to the repository
        /// </summary>
        public void SaveChanges()
        {
            document.Save(FILE_NAME);
        }

        /// <summary>
        /// Saves all changes made to the repository to the specified file.
        /// </summary>
        public void SaveChanges(string fileName)
        {
            document.Save(fileName);
            FILE_NAME = fileName;
        }

        /// <summary>
        /// Loads a new file into the repository.
        /// </summary>
        public void LoadData(string fileName)
        {
            this.document = XDocument.Load(fileName);
            FILE_NAME = fileName;
        }

        /// <summary>
        /// Checks if the repository has any changes
        /// </summary>
        /// <returns></returns>
        public bool HasChanges()
        {
            if (!File.Exists(FILE_NAME))
            {
                return this.document.Root.HasElements;
            }
            var originalDocument = XDocument.Load(FILE_NAME);
            return !XNode.DeepEquals(originalDocument, this.document);
        }

        #endregion


    }
}
