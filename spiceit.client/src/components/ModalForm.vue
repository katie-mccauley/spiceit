<template>
  <div
    class="modal fade"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content bg-transparent border-0 text-light">
        <div class="modal-header">
          <h5 class="title">
            <slot name="title"><!--title slot--></slot>
          </h5>
          <button
            type="button"
            class="btn-close btn-close-white"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <slot name="body"><!--body slot--></slot>
        </div>
        <div class="modal-footer">
          <slot name="footer"><!--footer slot--></slot>
        </div>
      </div>
    </div>
  </div>
</template>