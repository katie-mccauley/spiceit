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
    @submit.prevent="createIngredient"
  >
    <!-- <div class="input-group mb-3">
      <input
        v-model="editable.name"
        required
        type="text"
        class="form-control"
        placeholder="Add Name"
        aria-label="Recipient's username"
      />
      <input
        v-model="editable.quantity"
        required
        type="text"
        class="form-control"
        placeholder="Add Quantity"
        aria-label="Recipient's username"
      />

      <button
        class="btn btn-outline-secondary"
        type="button"
        id="button-addon2"
      >
        Create
      </button> -->
    <!-- </div> -->
    <div class="row g-3">
      <div class="col-6">
        <input
          v-model="editable.name"
          required
          type="text"
          class="form-control"
          aria-describedby="helpId"
          placeholder="Name"
        />
      </div>
      <div class="col-3">
        <input
          v-model="editable.quantity"
          required
          type="text"
          class="form-control"
          aria-describedby="helpId"
          placeholder="Quantity"
        />
      </div>
      <div class="col-2 d-flex justify-content-end align-items-end">
        <button class="btn p-0 m-0"><h4>+</h4></button>
      </div>
    </div>
  </form>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { useRoute } from "vue-router"
import { ingredientsService } from "../services/IngredientsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
export default {
  props: {
    idata: {
      type: Object,
      required: false
    }
  },
  setup(props) {
    const editable = ref({})
    const route = useRoute()
    return {
      editable,
      async createIngredient() {
        try {
          editable.value.recipeId = props.idata.id
          await ingredientsService.createIngredient(editable.value)
          editable.value = {}
        } catch (error) {
          logger.error(error)
          Pop.toast('error')
        }

      },
      active: computed(() => AppState.activeRecipe)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>