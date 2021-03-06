﻿using Model.Register;
using System.Collections.Generic;


namespace Model.Tables
{
    public class Category
    {
        public long? CategoryID { get; set; }
        public string Name { get; set; }
        //[NotMapped]
        public virtual ICollection<Product> products { get; set; }
    }
}
