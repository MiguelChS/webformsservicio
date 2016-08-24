using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormsService.Storage
{
    interface Storage
    {
        
        void Write<T>( T dataToSave );
        String Read();
    }
}
