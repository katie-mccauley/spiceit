<template>
  <div class="component">
    <div class="row">
      <i class="mdi mdi-heart selectable" @click="createFav(recipe.id)"></i>
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
      data-bs-target="#more-details"
      @click="activeRecipe(recipe.id)"
    >
      See more details
    </button>
  </div>

  <!-- <Modal :id="'moredetails' + recipe.id">
    <template #title> More Details</template>
    <template #body>
      <div class="container-fluid">
        <div class="row bg-white text-dark rounded">
          <div class="col-10" v-for="i in ingredients" :key="i.id">
            <h1>{{ i.name }}</h1>
          </div>
          <div class="col-10 text-dark" v-for="i in ingredients" :key="i.id">
            <h2>The ingredients</h2>
            <h3>{{ i.name }}: {{ i.quantity }}</h3>
            <i
              class="mdi mdi-delete selectable"
              @click="deleteIngredient(i.id)"
            ></i>
          </div>
          <Ingred />
        </div>
        <div class="row bg-white text-dark rounded">
          <div class="col-10 text-info" v-for="s in steps" :key="s.id">
            <h2>The steps for the recipe</h2>
            <h3>{{ s.ordr }}: {{ s.body }}</h3>
            <i class="mdi mdi-delete selectable" @click="deleteStep(s.id)"></i>
          </div>
        </div>
      </div>
    </template>
  </Modal> -->
</template>


<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { ingredientsService } from "../services/IngredientsService"
import { AppState } from "../AppState"
import { stepsService } from "../services/StepsService"
import { recipesService } from "../services/RecipesService"
import { favoritesService } from "../services/FavoritesService"
export default {
  props: {
    recipe: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    // watchEffect(async () => {
    //   try {

    //   } catch (error) {
    //     logger.error(error)
    //   }
    // })
    return {
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps),
      account: computed(() => AppState.account),
      async deleteStep(id) {
        try {
          await stepsService.deleteStep(id)
        } catch (error) {
          logger.error(error)
        }
      },
      async deleteIngredient(id) {
        try {
          await ingredientsService.deleteIngredient(id)
        } catch (error) {
          logger.error(error)
          Pop.toast('error')
        }
      },
      async activeRecipe(id) {
        try {
          logger.log(id, props.recipe)
          await recipesService.activeRecipe(id)
          await stepsService.getAllSteps(id);
          await ingredientsService.getAllIngredients(id);
        } catch (error) {
          logger.error(error)
        }
      },
      async createFav(id) {
        let fav = {
          accountId: AppState.account.id,
          recipeId: id,

        }

        await favoritesService.createFav(fav)
      },
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