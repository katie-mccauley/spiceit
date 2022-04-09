<template>
  <div class="col-10 text-info">
    <h2>The ingredients for this recipe</h2>
    <h3>
      {{ ingredients.name }}: Will need
      {{ ingredients.quantity }}
    </h3>
    <form @submit.prevent="createIngredient">
      <div class="input-group mb-3">
        <input
          v-model="editable.name"
          type="text"
          class="form-control"
          placeholder="Add Name"
          aria-label="Recipient's username"
          aria-describedby="button-addon2"
        />
        <input
          v-model="editable.quantity"
          type="text"
          class="form-control"
          placeholder="Add Quantity"
          aria-label="Recipient's username"
          aria-describedby="button-addon2"
        />
        <button
          class="btn btn-outline-secondary"
          type="button"
          id="button-addon2"
        >
          Create
        </button>
      </div>
    </form>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { ingredientsService } from "../services/IngredientsService"
import { watchEffect } from "@vue/runtime-core"
export default {
  props: {
    ingredientData: {
      type: Object,
      required: false
    }
  },
  setup(props) {
    const editable = ref({})
    const route = useRoute()
    watchEffect(async () => {
      try {
        await ingredientsService.getAllIngredients(props.ingredientData.id);
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      editable,
      ingredients: computed(() => AppState.ingredients),
      async createIngredient() {
        try {
          editable.value.recipeId = props.ingredientData.id
          await ingredientsService.createIngredient(editable.value)
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