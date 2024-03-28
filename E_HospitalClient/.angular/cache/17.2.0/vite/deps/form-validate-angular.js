import {
  Directive,
  ElementRef,
  HostListener,
  Input,
  setClassMetadata,
  ɵɵdefineDirective,
  ɵɵdirectiveInject,
  ɵɵlistener
} from "./chunk-W2U6RJ4J.js";
import "./chunk-EHLZM3EC.js";

// node_modules/form-validate-angular/fesm2022/form-validate-angular.mjs
var _FormValidateDirective = class _FormValidateDirective {
  constructor(el) {
    this.el = el;
    this.customValidateMessage = true;
  }
  keyupOrSubmit(event) {
    this.checkValidation();
  }
  checkValidation() {
    for (let child in this.el.nativeElement.elements) {
      const childElement = this.el.nativeElement.elements[child];
      if (childElement.validity !== void 0) {
        const elName = "[name=" + childElement.name + "] + div";
        let divEl;
        if (childElement.name !== "") {
          divEl = document.querySelector(elName);
        }
        if (!childElement.validity.valid) {
          if (this.customValidateMessage && divEl !== null)
            divEl.innerHTML = childElement.validationMessage;
          childElement.classList.add("is-invalid");
        } else {
          childElement.classList.remove("is-invalid");
        }
      }
    }
  }
};
_FormValidateDirective.ɵfac = function FormValidateDirective_Factory(t) {
  return new (t || _FormValidateDirective)(ɵɵdirectiveInject(ElementRef));
};
_FormValidateDirective.ɵdir = ɵɵdefineDirective({
  type: _FormValidateDirective,
  selectors: [["", "formValidate", ""]],
  hostBindings: function FormValidateDirective_HostBindings(rf, ctx) {
    if (rf & 1) {
      ɵɵlistener("keyup", function FormValidateDirective_keyup_HostBindingHandler($event) {
        return ctx.keyupOrSubmit($event);
      })("submit", function FormValidateDirective_submit_HostBindingHandler($event) {
        return ctx.keyupOrSubmit($event);
      })("change", function FormValidateDirective_change_HostBindingHandler($event) {
        return ctx.keyupOrSubmit($event);
      });
    }
  },
  inputs: {
    customValidateMessage: "customValidateMessage"
  },
  standalone: true
});
var FormValidateDirective = _FormValidateDirective;
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(FormValidateDirective, [{
    type: Directive,
    args: [{
      selector: "[formValidate]",
      standalone: true
    }]
  }], () => [{
    type: ElementRef
  }], {
    customValidateMessage: [{
      type: Input,
      args: ["customValidateMessage"]
    }],
    keyupOrSubmit: [{
      type: HostListener,
      args: ["keyup", ["$event"]]
    }, {
      type: HostListener,
      args: ["submit", ["$event"]]
    }, {
      type: HostListener,
      args: ["change", ["$event"]]
    }]
  });
})();
export {
  FormValidateDirective
};
//# sourceMappingURL=form-validate-angular.js.map
