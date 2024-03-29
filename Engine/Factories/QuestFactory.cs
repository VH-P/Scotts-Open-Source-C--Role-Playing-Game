﻿using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Shared;
using System.IO;
using System.Xml;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Quests.xml";
        private static readonly List<Quest> _quests = new List<Quest>();
        static QuestFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data=new XmlDocument(); 
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadQuestsFromNodes(data.SelectNodes("/Quests/Quest"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }
        private static void LoadQuestsFromNodes(XmlNodeList nodes)
        {
            foreach(XmlNode node in nodes)
            {
                List<ItemQuantity> itemToComplete = new List<ItemQuantity>();
                List<ItemQuantity> rewardItems=new List<ItemQuantity>();
                foreach(XmlNode childNode in node.SelectNodes("./ItemToComplete/Item"))
                {
                    itemToComplete.Add(new ItemQuantity(childNode.AttributeAsInt("ID"), childNode.AttributeAsInt("Quantity")));
                }
                foreach(XmlNode childNode in node.SelectNodes("./RewardItems/Item"))
                {
                    rewardItems.Add(new ItemQuantity(childNode.AttributeAsInt("ID"), childNode.AttributeAsInt("Quantity")));
                }
                _quests.Add(new Quest(node.AttributeAsInt("ID"), node.SelectSingleNode("./Name")?.InnerText ?? "", node.SelectSingleNode("./Description")?.InnerText ?? "", itemToComplete, node.AttributeAsInt("RewardExperiencePoints"), node.AttributeAsInt("RewardGold"), rewardItems));
            }
        }
        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
