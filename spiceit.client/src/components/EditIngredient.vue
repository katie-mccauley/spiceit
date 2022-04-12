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
    @submit.prevent="editIngredient"
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
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Name: </label>
      <input
        v-model="editable.name"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="location....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Quantity: </label>
      <input
        v-model="editable.quantity"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="location....."
      />
    </div>
    <div class="col-12 d-flex justify-content-end">
      <button class="btn btn-info">Edit</button>
    </div>
  </form>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { ingredientsService } from "../services/IngredientsService"
import { Modal } from "bootstrap"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
export default {
  setup() {
    const editable = ref({})
    const route = useRoute()
    return {
      editable,
      async editIngredient() {
        try {
          Modal.getOrCreateInstance(document.getElementById("edit")).hide()
          logger.log(editable.value.id)
          await ingredientsService.editIngredient(editable.value, AppState.activeIngredient.id)
        } catch (error) {
          logger.error(error)
          Pop.toast('error')
        }
      },
      active: computed(() => AppState.activeIngredient)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>