using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.Directives;
using LogicBuilder.App.Maui.Forms.Configuration.ListForm;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Forms.Configuration.SearchForm;
using LogicBuilder.App.Maui.Forms.Configuration.TextForm;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.Bindings;
using LogicBuilder.App.Maui.Forms.Parameters.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters.Directives;
using LogicBuilder.App.Maui.Forms.Parameters.ListForm;
using LogicBuilder.App.Maui.Forms.Parameters.Navigation;
using LogicBuilder.App.Maui.Forms.Parameters.SearchForm;
using LogicBuilder.App.Maui.Forms.Parameters.TextForm;
using LogicBuilder.App.Maui.Forms.Parameters.Validation;

namespace LogicBuilder.App.Maui.AutoMapperProfiles
{
    public class FormsParameterToFormsDescriptorMappingProfile : Profile
    {
        private const string dataType = "dataType";
        private const string modelType = "modelType";
        private const string dataReturnType = "dataReturnType";
        private const string modelReturnType = "modelReturnType";
        private const string type = "type";
        public FormsParameterToFormsDescriptorMappingProfile()
        {
			CreateMap<DataFormSettingsParameters, DataFormSettingsDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<DirectiveArgumentParameters, DirectiveArgumentDescriptor>()
                .ForCtorParam(type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<DirectiveDefinitionParameters, DirectiveDefinitionDescriptor>();
			CreateMap<DirectiveParameters, DirectiveDescriptor>();
			CreateMap<DropDownItemBindingParameters, DropDownItemBindingDescriptor>();
            CreateMap<DropdownSelectorControlSettingsParameters, DropdownSelectorControlSettingsDescriptor>()
                .ForCtorParam(type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<DropDownTemplateParameters, DropDownTemplateDescriptor>();
			CreateMap<FieldValidationSettingsParameters, FieldValidationSettingsDescriptor>();
			CreateMap<FormattedLabelItemParameters, FormattedLabelItemDescriptor>();
			CreateMap<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
                .ForCtorParam(type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<FormGroupBoxSettingsParameters, FormGroupBoxSettingsDescriptor>();
			CreateMap<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<FormGroupTemplateParameters, FormGroupTemplateDescriptor>();
			CreateMap<FormRequestDetailsParameters, FormRequestDetailsDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
                .ForCtorParam(dataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName));
			CreateMap<FormsCollectionDisplayTemplateParameters, FormsCollectionDisplayTemplateDescriptor>();
			CreateMap<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>();
			CreateMap<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>();
            CreateMap<InputFieldControlSettingsParameters, InputFieldControlSettingsDescriptor>()
                .ForCtorParam(type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<LabelItemParameters, LabelItemDescriptor>();
			CreateMap<ListFormSettingsParameters, ListFormSettingsDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<MultiBindingParameters, MultiBindingDescriptor>();
			CreateMap<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>()
                .ForCtorParam(type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<MultiSelectItemBindingParameters, MultiSelectItemBindingDescriptor>();
			CreateMap<MultiSelectTemplateParameters, MultiSelectTemplateDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<NavigationBarParameters, NavigationBarDescriptor>();
			CreateMap<NavigationMenuItemParameters, NavigationMenuItemDescriptor>();
			CreateMap<RequestDetailsParameters, RequestDetailsDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
                .ForCtorParam(dataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName))
                .ForCtorParam(modelReturnType, opts => opts.MapFrom(x => x.ModelReturnType.AssemblyQualifiedName))
                .ForCtorParam(dataReturnType, opts => opts.MapFrom(x => x.DataReturnType.AssemblyQualifiedName));
			CreateMap<SearchFilterGroupParameters, SearchFilterGroupDescriptor>();
			CreateMap<SearchFilterParameters, SearchFilterDescriptor>();
			CreateMap<SearchFormSettingsParameters, SearchFormSettingsDescriptor>()
                .ForCtorParam(modelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<SpanItemParameters, SpanItemDescriptor>();
			CreateMap<TextFieldTemplateParameters, TextFieldTemplateDescriptor>();
			CreateMap<TextFormSettingsParameters, TextFormSettingsDescriptor>();
			CreateMap<TextGroupParameters, TextGroupDescriptor>();
			CreateMap<TextItemBindingParameters, TextItemBindingDescriptor>();
			CreateMap<ValidationMessageParameters, ValidationMessageDescriptor>();
			CreateMap<ValidationRuleParameters, ValidationRuleDescriptor>();
			CreateMap<ValidatorArgumentParameters, ValidatorArgumentDescriptor>()
                .ForCtorParam(type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ValidatorDefinitionParameters, ValidatorDefinitionDescriptor>();
			CreateMap<VariableDirectivesParameters, VariableDirectivesDescriptor>();

            CreateMap<IFormItemSettingsParameters, IFormItemSettingsDescriptor>()
				.Include<DropdownSelectorControlSettingsParameters, DropdownSelectorControlSettingsDescriptor>()
				.Include<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
				.Include<FormGroupBoxSettingsParameters, FormGroupBoxSettingsDescriptor>()
				.Include<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
                .Include<InputFieldControlSettingsParameters, InputFieldControlSettingsDescriptor>()
                .Include<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>();

            CreateMap<ISearchFilterParameters, SearchFilterDescriptorBase>()
				.Include<SearchFilterGroupParameters, SearchFilterGroupDescriptor>()
				.Include<SearchFilterParameters, SearchFilterDescriptor>();

            CreateMap<ILabelItemParameters, LabelItemDescriptorBase>()
				.Include<FormattedLabelItemParameters, FormattedLabelItemDescriptor>()
				.Include<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>()
				.Include<LabelItemParameters, LabelItemDescriptor>();

            CreateMap<ISpanItemParameters, SpanItemDescriptorBase>()
				.Include<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>()
				.Include<SpanItemParameters, SpanItemDescriptor>();

            CreateMap<ItemBindingParameters, ItemBindingDescriptor>()
				.Include<DropDownItemBindingParameters, DropDownItemBindingDescriptor>()
				.Include<MultiSelectItemBindingParameters, MultiSelectItemBindingDescriptor>()
				.Include<TextItemBindingParameters, TextItemBindingDescriptor>();
        }
    }
}