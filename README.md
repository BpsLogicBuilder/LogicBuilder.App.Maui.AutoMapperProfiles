# LogicBuilder.App.Maui.AutoMapperProfiles

[![CI](https://github.com/BpsLogicBuilder/LogicBuilder.App.Maui.AutoMapperProfiles/actions/workflows/ci.yml/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.App.Maui.AutoMapperProfiles/actions/workflows/ci.yml)
[![CodeQL](https://github.com/BpsLogicBuilder/LogicBuilder.App.Maui.AutoMapperProfiles/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.App.Maui.AutoMapperProfiles/actions/workflows/github-code-scanning/codeql)
[![codecov](https://codecov.io/github/BpsLogicBuilder/LogicBuilder.App.Maui.AutoMapperProfiles/graph/badge.svg?token=0UJRC5MEGK)](https://codecov.io/github/BpsLogicBuilder/LogicBuilder.App.Maui.AutoMapperProfiles)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=BpsLogicBuilder_LogicBuilder.App.Maui.AutoMapperProfiles&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=BpsLogicBuilder_LogicBuilder.App.Maui.AutoMapperProfiles)

## Overview

This library provides AutoMapper configuration profiles for mapping between form parameters and serializable descriptors in .NET MAUI applications using the Logic Builder framework.

## Purpose

`LogicBuilder.App.Maui.AutoMapperProfiles` serves as a centralized location for mapping profiles that transform form feature parameters (defined in the Logic Builder design environment) into runtime descriptors used by .NET MAUI applications. These mappings enable seamless conversion between configuration-time objects and runtime-ready descriptors.

## Key Features

- **Parameter-to-Descriptor Mappings**: Converts design-time parameters to runtime descriptors for:
  - **Data Forms**: Input fields, dropdown selectors, multi-select controls, form groups, and validation settings
  - **Search Forms**: Search filters and filter groups
  - **List Forms**: List view configurations
  - **Text Forms**: Labels, formatted text, spans, and hyperlinks
  - **Navigation**: Navigation bars and menu items
  - **Validation**: Validation rules, messages, and field validation settings
  - **Directives**: Conditional directives and variable directives
  - **Bindings**: Text item, dropdown item, and multi-select item bindings

- **Type Conversion**: Handles complex type conversions including:
  - Enum mappings (e.g., `FormType` parameters to descriptors)
  - Type serialization (e.g., `Type` objects to `AssemblyQualifiedName` strings)
  - Collection transformations
  - Nested object mappings

- **Connector Support**: Maps connector parameters (e.g., command buttons) with associated metadata

## Dependencies

- **AutoMapper** (v16.1.1): Core mapping functionality
- **LogicBuilder.App.Maui.Forms.Parameters** (v1.0.1): Source parameter types
- **LogicBuilder.App.Maui.Forms.Configuration** (v1.0.2): Target descriptor types

## Target Framework

- **.NET Standard 2.0**: Ensures broad compatibility across .NET implementations

## Supported Mappings

### Data Forms
- `DataFormSettingsParameters` → `DataFormSettingsDescriptor`
- `FormGroupSettingsParameters` → `FormGroupSettingsDescriptor`
- `FormGroupArraySettingsParameters` → `FormGroupArraySettingsDescriptor`
- `FormGroupBoxSettingsParameters` → `FormGroupBoxSettingsDescriptor`
- `InputFieldControlSettingsParameters` → `InputFieldControlSettingsDescriptor`
- `DropdownSelectorControlSettingsParameters` → `DropdownSelectorControlSettingsDescriptor`
- `MultiSelectFormControlSettingsParameters` → `MultiSelectFormControlSettingsDescriptor`

### Validation
- `ValidationMessageParameters` → `ValidationMessageDescriptor`
- `ValidationRuleParameters` → `ValidationRuleDescriptor`
- `FieldValidationSettingsParameters` → `FieldValidationSettingsDescriptor`
- `ValidatorDefinitionParameters` → `ValidatorDefinitionDescriptor`
- `ValidatorArgumentParameters` → `ValidatorArgumentDescriptor`

### Bindings
- `TextItemBindingParameters` → `TextItemBindingDescriptor`
- `DropDownItemBindingParameters` → `DropDownItemBindingDescriptor`
- `MultiSelectItemBindingParameters` → `MultiSelectItemBindingDescriptor`
- `MultiBindingParameters` → `MultiBindingDescriptor`

### Navigation
- `NavigationBarParameters` → `NavigationBarDescriptor`
- `NavigationMenuItemParameters` → `NavigationMenuItemDescriptor`

### Other Components
- `CommandButtonParameters` → `CommandButtonDescriptor`
- `RequestDetailsParameters` → `RequestDetailsDescriptor`
- `FormRequestDetailsParameters` → `FormRequestDetailsDescriptor`
- Templates (TextFieldTemplate, DropDownTemplate, MultiSelectTemplate, etc.)
- Directives (DirectiveDefinition, DirectiveArgument, VariableDirectives)

## Testing

The library includes comprehensive unit tests (61+ tests) that verify:
- Property initialization and mapping accuracy
- Null handling for optional fields
- Complex nested object mappings
- Collection transformations
- Type conversions and serialization

## Related Projects

- [LogicBuilder](https://github.com/BpsLogicBuilder/LogicBuilder): The main Logic Builder project
- [LogicBuilder.App.Maui.Forms.Parameters](https://github.com/BpsLogicBuilder/LogicBuilder.App.Maui.Forms.Parameters): Source parameter definitions
- [LogicBuilder.App.Maui.Forms.Configuration](https://github.com/BpsLogicBuilder/LogicBuilder.App.Maui.Forms.Configuration): Target descriptor definitions

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

