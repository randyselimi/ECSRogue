using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using ECSRogue.Components;

namespace ECSRogue.Data
{
    /// <summary>
    /// Defines an entity, its components, and the values of its components
    /// </summary>
    public class EntityDefinition
    {
        public string Id { get; }
        public List<Component> components = new List<Component>();

        public EntityDefinition(string Id)
        {
            this.Id = Id;
        }
    }

    /// <summary>
    /// Represents a component as defined in Xml and the data needed to initialize it as an object
    /// </summary>
    public class ComponentDefinition
    {
        public string Id { get; }
        public List<ComponentData> data = new List<ComponentData>();

        public ComponentDefinition(string id)
        {
            Id = id;
        }
    }

    public class ComponentData
    {
        public string Id { get; }
        public object Data { get; }

        public ComponentData(string id, object data)
        {
            Id = id;
            Data = data;
        }
    }

    /// <summary>
    /// Combine EntityDefinitionLoader and EntityDefintinonManagree
    /// </summary>
    public class EntityDefinitionManager
    {
        private Dictionary<string, EntityDefinition> entityDefinitions = new Dictionary<string, EntityDefinition>();

        public EntityDefinitionManager()
        {
        }

        public void Initialize(Dictionary<string, EntityDefinition> entityDefinitions)
        {
            this.entityDefinitions = entityDefinitions;
        }
        public void AddDefinition(EntityDefinition entityDefinition)
        {
            entityDefinitions.Add(entityDefinition.Id, entityDefinition);
        }
        public EntityDefinition GetDefinition(string entityDefinitionId)
        {
            return entityDefinitions[entityDefinitionId];
        }
    }
    public class EntityDefinitionLoader
    {
        private SpriteAtlas spriteAtlas;

        public EntityDefinitionLoader(SpriteAtlas atlas)
        {
            spriteAtlas = atlas;
        }
        /// <summary>
        /// TODO add support for unnested component data
        /// </summary>
        /// <param name="entityPath"></param>
        /// <returns></returns>
        public Dictionary<string, EntityDefinition> LoadEntityDefinitions(string entityPath)
        {
            Dictionary<string, EntityDefinition> definitions = new Dictionary<string, EntityDefinition>();
            XmlDocument document = new XmlDocument();

            //Temporary
            string filepath = System.IO.Path.GetFullPath(@"..\..\..\Data");
            document.Load(Path.Combine(filepath, "EntityDefinitions.xml"));

            XmlNode root = document.DocumentElement;
            XmlNodeList entityList = root.SelectNodes("/EntityDefinitions/Entity");

            foreach (XmlNode entityNode in entityList)
            {
                EntityDefinition entityDefinition = new EntityDefinition(entityNode.Attributes[0].Value);

                XmlNodeList componentList = entityNode.SelectNodes("Components/*");

                foreach (XmlNode componentNode in componentList)
                {
                    ComponentDefinition definition = new ComponentDefinition(componentNode.LocalName);

                    XmlNodeList componentData = componentNode.SelectNodes("*");

                    foreach (XmlNode data in componentData)
                    {
                        definition.data.Add(new ComponentData(data.LocalName, data.InnerText));
                    }

                    entityDefinition.components.Add(InitializeComponent(definition));

                }

                definitions.Add(entityDefinition.Id, entityDefinition);
            }

            return definitions;
        }

        public Component InitializeComponent(ComponentDefinition definition)
        {
            Type t = Type.GetType("ECSRogue.Components." + definition.Id);

            Component newComponent;
            //Checks if the component being created has parameter data that needs to be passed to its constructor
            if (typeof(IParameterizedComponent).IsAssignableFrom(t))
            {
                newComponent = (Component)Activator.CreateInstance(t, definition.data);

                if (newComponent is IContentComponent)
                {
                    (newComponent as IContentComponent).LoadContent(spriteAtlas);
                }
            }
            //Otherwise the component has a parameterless constructor
            else
            {
                newComponent = (Component) Activator.CreateInstance(t);
            }

            return newComponent;
        }

    }
}
