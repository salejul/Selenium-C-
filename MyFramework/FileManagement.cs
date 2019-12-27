using System;
using System.Collections.Generic;
using System.IO;

namespace MyFramework
{
    class FileManagement
    {

        private string _fileName = null;

        public string FileName
        {
            get { return _fileName; }
            set { this._fileName = value; }
        }

        public void Store(string LogMessage)
        {
            using (StreamWriter fileHandle = new StreamWriter(this._fileName, true))
            {
                fileHandle.WriteLine(LogMessage);
            }
        }

        public string[] Read()
        {
            List<string> listOfNames = new List<string>();
            using (StreamReader fileHandle = new StreamReader(this._fileName))
            {
                string fileContents = "";
                while ((fileContents = fileHandle.ReadLine()) != null)
                {
                    listOfNames.Add(fileContents);
                }
            }
            return listOfNames.ToArray();
        }
    }
}
