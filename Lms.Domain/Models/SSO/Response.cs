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
    public partial class Response
    {

        private string issuerField;

        private ResponseStatus statusField;

        private Assertion assertionField;

        private string idField;

        private decimal versionField;

        private System.DateTime issueInstantField;

        private string destinationField;

        private string inResponseToField;

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
        public ResponseStatus Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
        public Assertion Assertion
        {
            get
            {
                return this.assertionField;
            }
            set
            {
                this.assertionField = value;
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
        public System.DateTime IssueInstant
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Destination
        {
            get
            {
                return this.destinationField;
            }
            set
            {
                this.destinationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InResponseTo
        {
            get
            {
                return this.inResponseToField;
            }
            set
            {
                this.inResponseToField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:protocol")]
    public partial class ResponseStatus
    {

        private ResponseStatusStatusCode statusCodeField;

        /// <remarks/>
        public ResponseStatusStatusCode StatusCode
        {
            get
            {
                return this.statusCodeField;
            }
            set
            {
                this.statusCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:protocol")]
    public partial class ResponseStatusStatusCode
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class Assertion
    {

        private string issuerField;

        private Signature signatureField;

        private AssertionSubject subjectField;

        private AssertionConditions conditionsField;

        private AssertionAttribute[] attributeStatementField;

        private AssertionAuthnStatement authnStatementField;

        private string idField;

        private System.DateTime issueInstantField;

        private decimal versionField;

        /// <remarks/>
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
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }

        /// <remarks/>
        public AssertionSubject Subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
            }
        }

        /// <remarks/>
        public AssertionConditions Conditions
        {
            get
            {
                return this.conditionsField;
            }
            set
            {
                this.conditionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Attribute", IsNullable = false)]
        public AssertionAttribute[] AttributeStatement
        {
            get
            {
                return this.attributeStatementField;
            }
            set
            {
                this.attributeStatementField = value;
            }
        }

        /// <remarks/>
        public AssertionAuthnStatement AuthnStatement
        {
            get
            {
                return this.authnStatementField;
            }
            set
            {
                this.authnStatementField = value;
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
        public System.DateTime IssueInstant
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class Signature
    {

        private SignatureSignedInfo signedInfoField;

        private string signatureValueField;

        private SignatureKeyInfo keyInfoField;

        /// <remarks/>
        public SignatureSignedInfo SignedInfo
        {
            get
            {
                return this.signedInfoField;
            }
            set
            {
                this.signedInfoField = value;
            }
        }

        /// <remarks/>
        public string SignatureValue
        {
            get
            {
                return this.signatureValueField;
            }
            set
            {
                this.signatureValueField = value;
            }
        }

        /// <remarks/>
        public SignatureKeyInfo KeyInfo
        {
            get
            {
                return this.keyInfoField;
            }
            set
            {
                this.keyInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfo
    {

        private SignatureSignedInfoCanonicalizationMethod canonicalizationMethodField;

        private SignatureSignedInfoSignatureMethod signatureMethodField;

        private SignatureSignedInfoReference referenceField;

        /// <remarks/>
        public SignatureSignedInfoCanonicalizationMethod CanonicalizationMethod
        {
            get
            {
                return this.canonicalizationMethodField;
            }
            set
            {
                this.canonicalizationMethodField = value;
            }
        }

        /// <remarks/>
        public SignatureSignedInfoSignatureMethod SignatureMethod
        {
            get
            {
                return this.signatureMethodField;
            }
            set
            {
                this.signatureMethodField = value;
            }
        }

        /// <remarks/>
        public SignatureSignedInfoReference Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoCanonicalizationMethod
    {

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoSignatureMethod
    {

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReference
    {

        private SignatureSignedInfoReferenceTransform[] transformsField;

        private SignatureSignedInfoReferenceDigestMethod digestMethodField;

        private string digestValueField;

        private string uRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
        public SignatureSignedInfoReferenceTransform[] Transforms
        {
            get
            {
                return this.transformsField;
            }
            set
            {
                this.transformsField = value;
            }
        }

        /// <remarks/>
        public SignatureSignedInfoReferenceDigestMethod DigestMethod
        {
            get
            {
                return this.digestMethodField;
            }
            set
            {
                this.digestMethodField = value;
            }
        }

        /// <remarks/>
        public string DigestValue
        {
            get
            {
                return this.digestValueField;
            }
            set
            {
                this.digestValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReferenceTransform
    {

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReferenceDigestMethod
    {

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureKeyInfo
    {

        private SignatureKeyInfoX509Data x509DataField;

        /// <remarks/>
        public SignatureKeyInfoX509Data X509Data
        {
            get
            {
                return this.x509DataField;
            }
            set
            {
                this.x509DataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureKeyInfoX509Data
    {

        private string x509CertificateField;

        /// <remarks/>
        public string X509Certificate
        {
            get
            {
                return this.x509CertificateField;
            }
            set
            {
                this.x509CertificateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionSubject
    {

        private AssertionSubjectNameID nameIDField;

        private AssertionSubjectSubjectConfirmation subjectConfirmationField;

        /// <remarks/>
        public AssertionSubjectNameID NameID
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
        public AssertionSubjectSubjectConfirmation SubjectConfirmation
        {
            get
            {
                return this.subjectConfirmationField;
            }
            set
            {
                this.subjectConfirmationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionSubjectNameID
    {

        private string formatField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Format
        {
            get
            {
                return this.formatField;
            }
            set
            {
                this.formatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionSubjectSubjectConfirmation
    {

        private AssertionSubjectSubjectConfirmationSubjectConfirmationData subjectConfirmationDataField;

        private string methodField;

        /// <remarks/>
        public AssertionSubjectSubjectConfirmationSubjectConfirmationData SubjectConfirmationData
        {
            get
            {
                return this.subjectConfirmationDataField;
            }
            set
            {
                this.subjectConfirmationDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Method
        {
            get
            {
                return this.methodField;
            }
            set
            {
                this.methodField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionSubjectSubjectConfirmationSubjectConfirmationData
    {

        private string inResponseToField;

        private System.DateTime notOnOrAfterField;

        private string recipientField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InResponseTo
        {
            get
            {
                return this.inResponseToField;
            }
            set
            {
                this.inResponseToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime NotOnOrAfter
        {
            get
            {
                return this.notOnOrAfterField;
            }
            set
            {
                this.notOnOrAfterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Recipient
        {
            get
            {
                return this.recipientField;
            }
            set
            {
                this.recipientField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionConditions
    {

        private AssertionConditionsAudienceRestriction audienceRestrictionField;

        private System.DateTime notBeforeField;

        private System.DateTime notOnOrAfterField;

        /// <remarks/>
        public AssertionConditionsAudienceRestriction AudienceRestriction
        {
            get
            {
                return this.audienceRestrictionField;
            }
            set
            {
                this.audienceRestrictionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime NotBefore
        {
            get
            {
                return this.notBeforeField;
            }
            set
            {
                this.notBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime NotOnOrAfter
        {
            get
            {
                return this.notOnOrAfterField;
            }
            set
            {
                this.notOnOrAfterField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionConditionsAudienceRestriction
    {

        private string audienceField;

        /// <remarks/>
        public string Audience
        {
            get
            {
                return this.audienceField;
            }
            set
            {
                this.audienceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionAttribute
    {

        private string attributeValueField;

        private string nameField;

        /// <remarks/>
        public string AttributeValue
        {
            get
            {
                return this.attributeValueField;
            }
            set
            {
                this.attributeValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionAuthnStatement
    {

        private AssertionAuthnStatementAuthnContext authnContextField;

        private System.DateTime authnInstantField;

        private string sessionIndexField;

        /// <remarks/>
        public AssertionAuthnStatementAuthnContext AuthnContext
        {
            get
            {
                return this.authnContextField;
            }
            set
            {
                this.authnContextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime AuthnInstant
        {
            get
            {
                return this.authnInstantField;
            }
            set
            {
                this.authnInstantField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SessionIndex
        {
            get
            {
                return this.sessionIndexField;
            }
            set
            {
                this.sessionIndexField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class AssertionAuthnStatementAuthnContext
    {

        private string authnContextClassRefField;

        /// <remarks/>
        public string AuthnContextClassRef
        {
            get
            {
                return this.authnContextClassRefField;
            }
            set
            {
                this.authnContextClassRefField = value;
            }
        }
    }
}
