﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SerializeAndDeserialize;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SerializeAndDeserializeWeb
{
    public partial class ProductCRUD : System.Web.UI.Page
    {
        string message;
        int nodeCount = 0;
        public void AlertClear()
        {
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += Request.Url.AbsoluteUri;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        }
        List<product> ListProducts = new List<product>();
        string XmlFileFullPath = @"C:/Users/ccruz1/Documents/visual studio 2015/Projects/SerializeAndDeserialize/SerializeAndDeserializeWeb/ProductWEB.xml";

        protected void Page_Load(object sender, EventArgs e)
        {
            string NodeType, NodeName;
            XmlTextReader reader = new XmlTextReader("http://afs-sl-schmgr03.afservice.org:8080/searchManager/search/afs-sl-schmstr.afservice.org:8080/solr4/Products/select?q=laptop&fl=EDP&store=pcmall&rows=25&start=0&facet=true&facet.field=Manufacturer&facet.field=InStock&facet.limit=10");
            reader.WhitespaceHandling = WhitespaceHandling.Significant;
            while (reader.Read())
            {

                NodeType = reader.NodeType.ToString() ; NodeName = reader.Name;
                // Print out info on node  
                Response.Write("Node Type: " + NodeType + "</br>");
                Response.Write("Node Name: " + NodeName + "</br></br>");
                Response.Write("");
            }
            BindProdGrid();
        }

        public void BindProdGrid()
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(List<product>));
            StreamReader SR = new StreamReader(@"C:/Users/ccruz1/Documents/visual studio 2015/Projects/SerializeAndDeserialize/SerializeAndDeserializeWeb/ProductWEB.xml");
            List<product> listproduct = (List<product>)xmlserializer.Deserialize(SR);
            foreach (product product in listproduct)
            {
                /*Response.Write("</br>");
                Response.Write(" ID        : " + product.ID + "</br>");
                Response.Write(" Name      : " + product.Name + "</br>");
                Response.Write(" Category  : " + product.CategoryName + "</br>");
                Response.Write(" Price     : " + product.price.Value + "</br>");
                Response.Write(" Unit      : " + product.price.Unit + "</br>");
                Response.Write(" Color     : " + product.description.Color + "</br>");
                Response.Write(" Size      : " + product.description.Size + "</br>");
                Response.Write(" Weight    : " + product.description.Weight + "</br>");*/
            }
            SR.Close();
            _grdvwProduct.DataBind();
        }

        protected void _btnSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(XmlFileFullPath))
            {   //Not Existing and adding one Record
                //List Serialize
                List<product> ListProducts = new List<product>();
                ListProducts.Add(new product
                {
                    ID = _txtID.Text.Trim(),
                    Name = _txtName.Text.Trim(),
                    CategoryName = _txtCategory.Text.Trim(),
                    price = new Price { Value = Convert.ToInt32(_txtPrice.Text), Unit = _txtUnit.Text.Trim() },
                    description = new Description { Color = _txtColor.Text.Trim(), Size = _txtSize.Text.Trim(), Weight = _txtWeight.Text.Trim() }
                });
                XmlSerializer xmlserializer = new XmlSerializer(typeof(List<product>));
                string PathName = "C:/Users/ccruz1/Documents/visual studio 2015/Projects/SerializeAndDeserialize/SerializeAndDeserializeWeb/";
                StreamWriter SW = new StreamWriter(PathName + "ProductWEB.xml");
                foreach (product product in ListProducts)
                {
                    xmlserializer.Serialize(SW, ListProducts);
                }
                SW.Close();
                //End of List Serizalize
            }
            else
            {   //Existing and Counting the records
                //Node Count
                using (var reader = XmlReader.Create(XmlFileFullPath))
                { while (reader.Read()) { if (reader.NodeType == XmlNodeType.Element && reader.Name == "name") { nodeCount++; } } }
                //End of Node Count
                if (nodeCount >= 0)
                { //Records exist and append the new data
                    XmlDocument XDOC = new XmlDocument();
                    XDOC.Load(XmlFileFullPath);
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(product));
                    XmlNode XNODE = XDOC.CreateNode(XmlNodeType.Element, "ArrayOfProduct", null);
                    XmlSerializerNamespaces NS = new XmlSerializerNamespaces();
                    StringWriter StringWriter = new StringWriter();
                    NS.Add("", "");
                    XmlWriterSettings XWriterSetting = new XmlWriterSettings();
                    XWriterSetting.OmitXmlDeclaration = true;
                    product product = new product
                    {
                        ID = _txtID.Text.Trim(),
                        Name = _txtName.Text.Trim(),
                        CategoryName = _txtCategory.Text.Trim(),
                        price = new Price { Value = Convert.ToInt32(_txtPrice.Text), Unit = _txtUnit.Text.Trim() },
                        description = new Description { Color = _txtColor.Text.Trim(), Size = _txtSize.Text.Trim(), Weight = _txtWeight.Text.Trim() }
                    };
                    using (XmlWriter XWriter = XmlWriter.Create(StringWriter, XWriterSetting))
                    {
                        xmlserializer.Serialize(XWriter, product, NS);
                    }
                    XNODE.InnerXml = Convert.ToString(StringWriter);
                    XmlNode XNODEBind = XNODE.SelectSingleNode("product");
                    XDOC.DocumentElement.AppendChild(XNODEBind);
                    XDOC.Save(XmlFileFullPath);
                    message = "Saved"; AlertClear();
                }
                else
                {   //Not Existing and adding one Record
                    //List Serialize
                    List<product> ListProducts = new List<product>();
                    ListProducts.Add(new product
                    {
                        ID = _txtID.Text.Trim(),
                        Name = _txtName.Text.Trim(),
                        CategoryName = _txtCategory.Text.Trim(),
                        price = new Price { Value = Convert.ToInt32(_txtPrice.Text), Unit = _txtUnit.Text.Trim() },
                        description = new Description { Color = _txtColor.Text.Trim(), Size = _txtSize.Text.Trim(), Weight = _txtWeight.Text.Trim() }
                    });
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(List<product>));
                    string PathName = "C:/Users/ccruz1/Documents/visual studio 2015/Projects/SerializeAndDeserialize/SerializeAndDeserializeWeb/";
                    StreamWriter SW = new StreamWriter(PathName + "ProductWEB.xml");
                    foreach (product product in ListProducts)
                    {
                        xmlserializer.Serialize(SW, ListProducts);
                    }
                    SW.Close();
                    //End of List Serizalize
                }
            }
            message = "Saved! " + nodeCount + " Total Records"; AlertClear();


            /*//List Serialize
            List<Product> ListProducts = new List<Product>();
            ListProducts.Add(new Product
            {
                ID = _txtID.Text.Trim(),
                Name = _txtName.Text.Trim(),
                CategoryName = _txtCategory.Text.Trim(),
                price = new Price { Value = Convert.ToInt32(_txtPrice.Text), Unit = _txtUnit.Text.Trim() },
                description = new Description { Color = _txtColor.Text.Trim(), Size = _txtSize.Text.Trim(), Weight = _txtWeight.Text.Trim() }
            });
            XmlSerializer xmlserializer = new XmlSerializer(typeof(List<Product>));
            string PathName = "C:/Users/ccruz1/Documents/visual studio 2015/Projects/SerializeAndDeserialize/SerializeAndDeserializeWeb/";
            StreamWriter SW = new StreamWriter(PathName + "ProductWEB.xml");
            foreach (Product product in ListProducts)
            {
                xmlserializer.Serialize(SW, ListProducts);
            }
            SW.Close();*/
            //End of List Serizalize


            /*//Serialize One Item only        

             Product product = new Product
             {
                 ID = _txtID.Text.Trim(),
                 Name = _txtName.Text.Trim(),
                 CategoryName = _txtCategory.Text.Trim(),
                 price = new Price { Value = Convert.ToInt32(_txtPrice.Text), Unit = _txtUnit.Text.Trim() },
                 description = new Description { Color = _txtColor.Text.Trim(), Size = _txtSize.Text.Trim(), Weight = _txtWeight.Text.Trim() }
             };
             XmlSerializer xmlserializer = new XmlSerializer(typeof(Product));
             string PathName = "C:/Users/ccruz1/Documents/visual studio 2015/Projects/SerializeAndDeserialize/SerializeAndDeserializeWeb/";
             StreamWriter SW = new StreamWriter(PathName+"ProductWEB.xml");
             xmlserializer.Serialize(SW, product);
             SW.Close();
             ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Save');", true);

            //End of Serialize One Item only*/
        }
    }
}