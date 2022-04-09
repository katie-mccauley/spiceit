<template>
  <div class="component">
    <div class="row">
      <img
        class="img-fluid cropped rounded"
        :src="recipe.picture"
        alt="recipe picture"
      />
    </div>
    <h2>{{ recipe.title }}</h2>
    <button
      class="btn btn-success"
      data-bs-toggle="modal"
      :data-bs-target="'#moredetails' + recipe.id"
    >
      See more details
    </button>
  </div>
  <Modal :id="'moredetails' + recipe.id">
    <template #title> More Details</template>
    <template #body>
      <div class="container-fluid">
        <div class="row bg-white text-dark rounded">
          <!-- <div class="col-10" v-for="i in ingredients" :key="i.id">
            <h1>{{ i.name }}</h1>
          </div> -->
          <div
            v-if="recipe.id == ingredients.recipeId"
            class="col-10 text-info"
          >
            <h2>The ingredients for this recipe</h2>
            <h3>
              {{ ingredients.name }}: Will need
              {{ ingredients.quantity }}
            </h3>
          </div>
        </div>
        <div class="row bg-white text-dark rounded">
          <div class="col-10 text-info">
            <h2>The steps for the recipe</h2>
            <h3>{{ steps.ordr }}: {{ steps.body }}</h3>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { ingredientsService } from "../services/IngredientsService"
import { AppState } from "../AppState"
import { stepsService } from "../services/StepsService"
export default {
  props: {
    recipe: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    watchEffect(async () => {
      try {
        await ingredientsService.getAllIngredients(props.recipe.id);
        await stepsService.getAllSteps(props.recipe.id);
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.cropped {
  height: 300px;
  position: center;
  display: cover;
  object-fit: cover;
}
</style>