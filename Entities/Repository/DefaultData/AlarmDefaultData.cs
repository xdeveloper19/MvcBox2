using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Repository.DefaultData
{
    public static class AlarmDefaultData
    {
        public static Dictionary<int, string> Messages = new Dictionary<int, string>()
        {
            {1, "Превышение веса"},
            {2, "Несанкционированное срабатывание концевого датчика роллеты"},
            {3, "Протечка жидкости"}
        };

        public static Dictionary<int, Guid> TypeId = new Dictionary<int, Guid>()
        {
            {1, new Guid("2798bd29-add0-4635-b8cc-8ef53cbe0835") },
            {2, new Guid("2e1f6fa6-3204-4bb2-9991-9bd13cb56751") },
            {3, new Guid("b1958c7e-9102-4a92-8879-92b041c8410a") }
        };
    }
}
