using System;
using System.IO;

namespace CongoSchool.Helpers
{
	public interface IFileService
	{
        void SavePicture(string name, Stream data, string location = "temp");
    }
}

