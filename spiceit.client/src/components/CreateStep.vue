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
    @submit.prevent="createStep"
  >
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Order: </label>
      <input
        v-model="editable.ordr"
        required
        type="number"
        class="form-control"
        aria-describedby="helpId"
        placeholder="location....."
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
        placeholder="location....."
      />
    </div>
    <div class="col-12 d-flex justify-content-end">
      <button class="btn btn-primary">create</button>
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