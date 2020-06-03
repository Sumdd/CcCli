using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model_v1 {
    public class updateJsonModel {
        public string version {
            get; set;
        }
        public List<updateFiles> files {
            get; set;
        }
    }

    public class updateFiles {
        public string filename {
            get; set;
        }
    }
}
