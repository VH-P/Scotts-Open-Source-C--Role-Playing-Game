using Engine.Models;
using Engine.Shared;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Engine.Factories
{
    public class RecipeFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Recipes.xml";
        private static readonly List<Recipe> _recipes = new List<Recipe>();
        static RecipeFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data= new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadRecipesFromNodes(data.SelectNodes("/Recipes/Recipe"));
            }
        }
        private static void LoadRecipesFromNodes(XmlNodeList nodes)
        {
            foreach(XmlNode node in nodes)
            {
                Recipe recipe = new Recipe(node.AttributeAsInt("ID"), node.SelectSingleNode("./Name")?.InnerText ?? "");
                foreach (XmlNode childNode in node.SelectNodes("./Ingredients/Item"))
                {
                    recipe.AddIngredient(childNode.AttributeAsInt("ID"), childNode.AttributeAsInt("Quantity"));
                }
                foreach(XmlNode chilNode in node.SelectNodes("./OutputItems/Item"))
                {
                    recipe.AddOutputItem(chilNode.AttributeAsInt("ID"), chilNode.AttributeAsInt("Quantity"));
                }
                _recipes.Add(recipe);
            }
        }
        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(x=>x.ID==id);
        }
    }
}
