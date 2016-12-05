﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebServiceDeserialization
{
    public partial class ProductList : System.Web.UI.Page
    {
        string Manufacturers = "";
        string DetailString = "";
        EDPList EDPList = new EDPList();
        Manufacturer Manufacturer = new Manufacturer();

        protected void Page_Load(object sender, EventArgs e)
        {

            //_rptrEDP.DataSource = EDPList.ListingEDP();
            //_rptrEDP.DataBind();
            _rdbtnlstManufact.DataSource = Manufacturer.ListManufacturer();
            _rdbtnlstManufact.DataBind();



            #region Working Code for Prod details
            //------------------------------------------WORKING STATIC

            List<string> ListEDP;
            List<string> ListManufact;
            ListManufact = Manufacturer.ListManufacturer();
            ListEDP = EDPList.ListingEDP();
            
            for (int i = 0; i < ListEDP.Count; i++)
            #region FORSTART
            {
                string URL = "http://afs-sl-pservice01.afservice.org:8080/productservice2/getProductInfo/pcmall?edplist=" + ListEDP[i] + "&ignoreCatalog=true";
                XmlTextReader reader = new XmlTextReader(URL);
                reader.WhitespaceHandling = WhitespaceHandling.Significant;
                DetailString += "<div class='row'><div class='col-sm-4'></div><div class='col-sm-4'><div class='panel panel-danger'><div class='panel-body'>";
                while (reader.Read())
                {
                    if (reader.Name == "name")
                    {
                        DetailString += "Name: ";
                        DetailString += reader.ReadElementString("name");
                        DetailString += "</br>";
                        DetailString += "</br>";
                    }
                    if (reader.Name == "manufacturer")
                    {
                        DetailString += "Manufacturer: ";
                        DetailString += reader.ReadElementString("manufacturer");
                        DetailString += "</br>";
                        DetailString += "</br>";
                    }
                    if (reader.Name == "description")
                    {
                        DetailString += "Description: ";
                        DetailString += reader.ReadElementString("description");
                        DetailString += "</br>";
                        DetailString += "</br>";
                    }
                    if (reader.Name == "finalPrice")
                    {
                        DetailString += "Final Price: ";
                        DetailString += reader.ReadElementString("finalPrice");
                        DetailString += "</br>";
                        DetailString += "</br>";
                    }
                    //if (reader.Name == "store")
                    //{
                    //    DetailString += "Store: ";
                    //    DetailString += reader.ReadElementString("store");
                    //    DetailString += "</br>";
                    //}
                    if (reader.Name == "availabilityDescription")
                    {
                        DetailString += "Availabilty: ";
                        DetailString += reader.ReadElementString("availabilityDescription");
                        DetailString += "</br>";
                        DetailString += "</br>";
                    }
                    if (reader.Name == "xlg")
                    {
                        string ImageURL = reader.ReadElementString("xlg");
                        DetailString += "Image URL: ";
                        DetailString += ImageURL + "</br>";
                        DetailString += "<img src =";
                        DetailString += '"' + ImageURL + '"';
                        DetailString += " class='img-responsive img-thumbnail center-block' width='150' height='150' alt='Image Not Available'>";
                        DetailString += "</br>";
                        DetailString += "</br>";
                    }
                }
                #endregion
                DetailString += "</div></div ></div ><div class='col-sm-4'></div></div>";
                DetailString += "</br>";
            }
            Response.Write(DetailString);
            //Response.Write(Manufacturers);
            Response.Write("</br></br></br>");

            //------------------------------------------WORKING STATIC
            #endregion
        }
    }
}