using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IntegraBussines
{
    public static class ProductsBLL
    {
        public static DataTable GetProducts(int company, string description, int marketing, int type)
        {
            return IntegraData.ProductsDAL.GetProducts(company, description, marketing, type);
        }

        public static DataTable GetConcepts(int company, int marketingproduct, int product, int customer, int to)
        {
            return IntegraData.ProductsDAL.GetConcepts(company, marketingproduct, product, customer, to);
        }
    }
}
