using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Respositories
{
    public interface IFileRepository
    {
        void CreateDirectory(string path);
        bool ExistDirectory(string path);
        void DeleteDirectory(string path, bool reculsive);

        void CreateFile(string path, string name);
        bool ExistFile(string path, string name);
        void DeleteFile(string path, string name);
    }
}
