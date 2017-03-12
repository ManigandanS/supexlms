using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.SSO
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:protocol")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:SAML:2.0:protocol", IsNullable = false)]
    public partial class LogoutRequest
    {

        private string issuerField;

        private string nameIDField;

        private string idField;

        private decimal versionField = 2.0m;

        private string issueInstantField;


        public LogoutRequest()
        {
            idField = "id" + Guid.NewGuid().ToString();
            issueInstantField = DateTime.UtcNow.ToString("o");
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
        public string Issuer
        {
            get
            {
                return this.issuerField;
            }
            set
            {
                this.issuerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
        public string NameID
        {
            get
            {
                return this.nameIDField;
            }
            set
            {
                this.nameIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IssueInstant
        {
            get
            {
                return this.issueInstantField;
            }
            set
            {
                this.issueInstantField = value;
            }
        }
    }


}
