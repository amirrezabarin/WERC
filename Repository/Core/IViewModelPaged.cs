﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IModelPaged<MP>
    {
        IEnumerable<MP> EntityList { get; set; }

        IEnumerable<MP> Select(Func<MP, bool> predicate, int index, int count);
        IEnumerable<MP> Select(int index, int count);
        int Count(Func<MP, bool> predicate);

    }
}
