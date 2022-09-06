using System;

namespace ToDoAppBLL.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string objName, string message):base(message){ }
    }
}
