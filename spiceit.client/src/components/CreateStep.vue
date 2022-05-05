<template>
  <form
    class="
      bg-grey
      darken-20
      justify-content-center
      elevation-3
      shadow
      col-12
      p-1
      rounded-bottom
    "
    @submit.prevent="createStep"
  >
    <div class="row">
      <div class="col-3">
        <input
          v-model="editable.ordr"
          required
          type="number"
          class="form-control"
          aria-describedby="helpId"
          placeholder="Order"
        />
      </div>
      <div class="col-6">
        <input
          v-model="editable.body"
          required
          type="text"
          class="form-control"
          aria-describedby="helpId"
          placeholder="Body"
        />
      </div>
      <div class="col-2 d-flex justify-content-end align-items-end">
        <button class="btn p-0 m-0"><h4>+</h4></button>
      </div>
    </div>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { stepsService } from "../services/StepsService"
export default {
  props: {
    sdata: {
      type: Object,
      required: false
    }
  },
  setup(props) {
    const editable = ref({})
    return {
      editable,
      async createStep() {
        try {
          editable.value.recipeId = props.sdata.id
          await stepsService.createStep(editable.value)
          editable.value = {}
        } catch (error) {
          logger.error(error)
          Pop.toast('error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>