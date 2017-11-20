/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for single Command data.
 * 
 * @created 2017-09-27 Artem Niikolaev
 * 
 * 
 * @info
 * 
 * Command format : 
 *                  Type   - Type of Command
 *                  Sender - Who send command
 *                  Reciver - Who must recive command
 *                  List of Data - Command specific data.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataDepositer
{
    //public enum CommandType
    //{
    //   None,
    //   TestConnect,
    //   Ready,
    //   StoreFile,
    //   SendFile,
    //   RequestFile,
    //   RequestChunk,
    //   CheckFile,
    //   CheckChunk
    //}

    [Serializable]
    public class Command
    {
        #region Public properties
        public CommandType Type = CommandType.None;
        public string Message { get => message; set => message = value; }
        public int Counter { get => counter; set => counter = value; }
        public string[] Args { get; set; }
        public object Data { get; set; } = null; // BLOB data of command
        #endregion

        #region Private members
        private string message;
        private int counter;
        #endregion

        #region Constructors
        public Command()
        {

        }

        public Command(string v1, int v2)
        {
            this.message = v1;
            this.counter = v2;
            Args = null;
        }

        public Command(CommandType type, string[] parameters)
        {
            Type = type;
            Args = parameters;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// params  : string command if specific xml format        
        /// </summary>
        /// <returns>true if success Parse string with command</returns>
        public bool Parse(string com)
        {
            bool res = false;

            if (com.Length > 0)
            {
                try
                {

                    res = true;
                }
                catch (Exception ex)
                {

                    //throw;
                }
            }

            return res;
        }
        #endregion

        public void xmltest()
        {
            List<string> commands = new List<string>();
            CommandType comtype = CommandType.None;

            StringBuilder output = new StringBuilder();

            String xmlString =
         @"<?xml version='1.0'?>
        <!-- This is a sample Command XML document -->
        <Command>
            <Type>None</Type>
            <Arguments>
                <Argument>11111111111</Argument>
                <Argument>22222222222</Argument>
                <Argument>33333333333</Argument>
                <Argument>44444444444</Argument>
                <Argument>55555555555</Argument>
                <Argument>66666666666</Argument>
            </Arguments>
        </Command>
         ";
            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(output, ws))
                {
                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }
                 }
            }
            Console.WriteLine(output.ToString());
            //OutputTextBlock.Text = output.ToString();
        }
    }
}
