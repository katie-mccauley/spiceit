<template>
  <form
    class="
      bg-grey
      darken-20
      justify-content-center
      elevation-3
      shadow
      col-8
      p-2
      m-5
    "
    @submit.prevent="editStep"
  >
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Order: </label>
      <input
        v-model="editable.ordr"
        required
        type="number"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Order"
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Body: </label>
      <input
        v-model="editable.body"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Body"
      />
    </div>
    <div class="col-12 d-flex justify-content-end">
      <button class="btn btn-primary">Edit</button>
    </div>
  </form>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { Modal } from "bootstrap"
import { stepsService } from "../services/StepsService"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async editStep() {
        try {
          Modal.getOrCreateInstance(document.getElementById("edit-step")).hide()
          await stepsService.editStep(editable.value, AppState.activeStep.id)
        } catch (error) {
          logger.error(error)
        }
      },
      activeStep: computed(() => AppState.activeStep)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>