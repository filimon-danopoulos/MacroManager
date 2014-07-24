﻿using MacroManager.Hooks;
using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MacroManager.Data
{
    /// <summary>
    /// Repository class for persisting Macros to an XML file. 
    /// </summary>
    public class XmlMacroRepository : IMacroRepository
    {
        #region Constants
        // TODO: Figure out a better way to handle these constants. Reflection maybe?
        private readonly string FILE_NAME;
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

        private const string LEFT_CLICK_ACTION_TYPE = "leftClickAction";
        private const string RIGHT_CLICK_ACTION_TYPE = "rightClickAction";
        private const string WAIT_ACTION_TYPE = "waitAction";

        #endregion

        #region Fields

        /// <summary>
        /// The XML document we are working with.
        /// </summary>
        private readonly XDocument document;

        #endregion

        #region Constructors

        /// <summary>
        /// Inializes the document field from a file. 
        /// Creates an in memory document if the file does not exist.
        /// </summary>
        public XmlMacroRepository(string fileName) : this(fileName, false){}

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
        /// Checks if the repository has any changes
        /// </summary>
        /// <returns></returns>
        public bool HasChanges()
        {
            var originalDocument = new XDocument(FILE_NAME);
            return !XNode.DeepEquals(originalDocument, this.document);
        }

        #endregion


    }
}