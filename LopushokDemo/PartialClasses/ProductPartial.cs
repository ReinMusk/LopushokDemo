using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LopushokDemo
{
    public partial class Product
    {
        public string Materials
        {
            get
            {
                string materials = "";
                foreach (var item in ProductMaterial.Where(x => x.ProductId == Id))
                {
                    materials += item.Material.Name.ToString() + ", ";
                }
                return materials;
            }
        }
        
    }
}
