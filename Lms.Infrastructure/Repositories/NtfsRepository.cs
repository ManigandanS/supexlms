using Lms.Domain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Infrastructure.Repositories
{
    public class NtfsRepository : IFileRepository
    {
        public void CreateDirectory(string path)
        {

        }

        public bool ExistDirectory(string path)
        {
            return true;
        }

        public void DeleteDirectory(string path, bool reculsive)
        {

        }

        public void CreateFile(string path, string name)
        {

        }

        public bool ExistFile(string path, string name)
        {
            return true;
        }

        public void DeleteFile(string path, string name)
        {

        }
    }
}
