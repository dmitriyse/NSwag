//-----------------------------------------------------------------------
// <copyright file="TypeScriptClientTemplateModel.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using NJsonSchema.CodeGeneration.TypeScript;

namespace NSwag.CodeGeneration.TypeScript.Models
{
    /// <summary>The TypeScript client template model.</summary>
    public class TypeScriptClientTemplateModel
    {
        private readonly SwaggerToTypeScriptClientGeneratorSettings _settings;
        private readonly SwaggerDocument _document;

        /// <summary>Initializes a new instance of the <see cref="TypeScriptClientTemplateModel" /> class.</summary>
        /// <param name="controllerClassName">Name of the controller.</param>
        /// <param name="operations">The operations.</param>
        /// <param name="document">The Swagger document.</param>
        /// <param name="settings">The settings.</param>
        public TypeScriptClientTemplateModel(
            string controllerClassName, 
            IEnumerable<TypeScriptOperationModel> operations, 
            SwaggerDocument document, 
            SwaggerToTypeScriptClientGeneratorSettings settings)
        {
            _settings = settings;
            _document = document;

            Class = controllerClassName;
            Operations = operations;
        }

        /// <summary>Gets the class name.</summary>
        public string Class { get; }

        /// <summary>Gets the client base class.</summary>
        public string ClientBaseClass => _settings.ClientBaseClass;

        /// <summary>Gets a value indicating whether the client class has a base class.</summary>
        public bool HasClientBaseClass => !string.IsNullOrEmpty(ClientBaseClass);

        /// <summary>Gets the configuration class name.</summary>
        public string ConfigurationClass => _settings.ConfigurationClass;

        /// <summary>Gets a value indicating whether the client class has a base class.</summary>
        public bool HasConfigurationClass => HasClientBaseClass && !string.IsNullOrEmpty(ConfigurationClass);

        /// <summary>Gets or sets a value indicating whether to call 'transformOptions' on the base class or extension class.</summary>
        public bool UseTransformOptionsMethod => _settings.UseTransformOptionsMethod;

        /// <summary>Gets or sets a value indicating whether to call 'transformResult' on the base class or extension class.</summary>
        public bool UseTransformResultMethod => _settings.UseTransformResultMethod;

        /// <summary>Gets a value indicating whether the client is extended with an extension class.</summary>
        public bool HasExtensionClass => _settings.TypeScriptGeneratorSettings.ExtendedClasses?.Any(c => c + "Base" == Class) == true;

        /// <summary>Gets a value indicating whether to generate optional parameters.</summary>
        public bool GenerateOptionalParameters => _settings.GenerateOptionalParameters;

        /// <summary>Gets a value indicating whether the client has operations.</summary>
        public bool HasOperations => Operations.Any();

        /// <summary>Gets the operations.</summary>
        public IEnumerable<TypeScriptOperationModel> Operations { get; }

        /// <summary>Gets a value indicating whether the client uses KnockoutJS.</summary>
        public bool UsesKnockout => _settings.TypeScriptGeneratorSettings.TypeStyle == TypeScriptTypeStyle.KnockoutClass;

        /// <summary>Gets the service base URL.</summary>
        public string BaseUrl => _document.BaseUrl;

        /// <summary>Gets a value indicating whether to generate client interfaces.</summary>
        public bool GenerateClientInterfaces => _settings.GenerateClientInterfaces;

        /// <summary>Gets the promise type.</summary>
        public string PromiseType => _settings.PromiseType == TypeScript.PromiseType.Promise ? "Promise" : "Q.Promise";

        /// <summary>Gets the promise constructor code.</summary>
        public string PromiseConstructor => _settings.PromiseType == TypeScript.PromiseType.Promise ? "new Promise" : "Q.Promise";

        /// <summary>Gets or sets a value indicating whether to use Aurelia HTTP injection.</summary>
        public bool UseAureliaHttpInjection => _settings.Template == TypeScriptTemplate.Aurelia;

        /// <summary>Gets a value indicating whether the target TypeScript version supports strict null checks.</summary>
        public bool SupportsStrictNullChecks => _settings.TypeScriptGeneratorSettings.TypeScriptVersion >= 2.0m;

        /// <summary>Gets or sets the token name for injecting the API base URL string (used in the Angular2 template).</summary>
        public string BaseUrlTokenName => _settings.BaseUrlTokenName;

        /// <summary>Gets or sets a value indicating whether DTO exceptions are wrapped in a SwaggerException instance.</summary>
        public bool WrapDtoExceptions => _settings.WrapDtoExceptions;
    }
}