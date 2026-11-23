// Views rendered into docs/design/generated/ for the Design document.
// Per SysML2Tools syntax, 'expose' is only valid inside a named 'view' usage (not a 'view def'
// definition), and it - not 'render' - is what scopes the rendered diagram content.
package SpdxModelDesignViews {
    private import StandardViewDefinitions::*;
    private import SpdxModel::*;

    view SoftwareStructureView : InterconnectionView {
        expose SpdxModel::SpdxModelSystem;
    }

    view SpdxModelView {
        expose SpdxModelSystem;
    }

    view IOView {
        expose IOSubsystem;
    }

    view TransformView {
        expose TransformSubsystem;
    }
}
