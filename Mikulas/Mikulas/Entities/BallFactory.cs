﻿using Mikulas.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikulas.Entities
{
    class BallFactory: IToyFactory
    {
        public toy CreateNew()
        {
            return new labda();
        }


    }
}
