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
    @submit.prevent="createRecipe"
  >
    <h2>Create Recipe</h2>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Title: </label>
      <input
        v-model="editable.title"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Name....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Subtitle: </label>
      <input
        v-model="editable.subtitle"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Description....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Picture: </label>
      <input
        v-model="editable.picture"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Cover Image....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Category: </label>
      <input
        v-model="editable.category"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="location....."
      />
    </div>

    <!-- <div class="col-md-4 mb-2">
      <label for="" class="form-label">type: </label>
      <input
        v-model="editable.type"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="type....."
      />
    </div> -->

    <div class="col-12 d-flex justify-content-end">
      <button class="btn btn-primary">create</button>
    </div>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { Modal } from "bootstrap"
import { recipesService } from "../services/RecipesService"
export default {
  setup() {
    const editable = ref({})
    const route = useRoute()
    return {
      editable,
      async createRecipe() {
        try {
          Modal.getOrCreateInstance(document.getElementById('create-recipe')).hide()
          await recipesService.createRecipe(editable.value);
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