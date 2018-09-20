namespace Twinfield_NewFramework
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ScenarioDataList
    {
        private ScenarioDataListScenario[] scenariosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Scenario", IsNullable = false)]
        public ScenarioDataListScenario[] Scenarios
        {
            get
            {
                return this.scenariosField;
            }
            set
            {
                this.scenariosField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ScenarioDataListScenario
    {
        private string nameField;

        private int iterationCountPerLogin;

        private decimal throughputField;
        
        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public int IterationCountPerLogin
        {
            get
            {
                return this.iterationCountPerLogin;
            }
            set
            {
                this.iterationCountPerLogin = value;
            }
        }

        /// <remarks/>
        public decimal Throughput
        {
            get
            {
                return this.throughputField;
            }
            set
            {
                this.throughputField = value;
            }
        }
        
    }
}