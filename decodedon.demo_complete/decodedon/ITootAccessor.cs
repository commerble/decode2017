using decodedon.Models;
using System.Collections.Generic;

namespace decodedon
{
    public interface ITootAccessor
    {
        IEnumerable<Toot> Load(int take, int skipToken = 0);
        bool Add(Toot toot);
    }
}
