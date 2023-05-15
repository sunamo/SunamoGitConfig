using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoGitConfig.Data
{
    public class ExistsNonExistsListGitConfig<T>
    {
        public List<T> Exists = new List<T>();
        public List<T> NonExists = new List<T>();
    }
}
