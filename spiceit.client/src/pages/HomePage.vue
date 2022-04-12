<template>
  <div class="container-fluid">
    <div class="row">
      <form class="form-group row" @submit.prevent="search">
        <input
          v-model="searchTerm"
          type="text"
          class="col-md-9 col-8"
          placeholder="search...."
        />
        <button class="btn btn-outline-primary col-4 col-md-3">search</button>
      </form>
      <div class="col-10 d-flex justify-content-between">
        <button class="btn-info btn" @click="getAll()">ALL</button>
        <button class="btn-info btn">Favorites</button>
        <button class="btn-info btn" @click="getFavs()">My Posts</button>
      </div>
      <div
        class="col-3 home-card p-2 m-2 bg-white rounded elevation-3 selectable"
        v-for="r in recipes"
        :key="r.id"
      >
        <div class="row justify-content-end">
          <button
            @click="deleteRecipe(r.id)"
            type="button"
            title="Delete Recipe"
            class="btn-close btn-close-dark me-2"
            aria-label="Close"
          ></button>
        </div>

        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
  <Modal id="create-recipe">
    <template #title> Create Recipe</template>
    <template #body><CreateRecipe :recipeData="recipe" /></template>
  </Modal>
  <Modal id="more-details">
    <template #title> More Details</template>
    <template #body>
      <div class="container-fluid">
        <div class="row bg-white text-dark rounded">
          <!-- <div class="col-10" v-for="i in ingredients" :key="i.id">
            <h1>{{ i.name }}</h1>
          </div> -->
          <h2>The ingredients</h2>
          <div class="col-10 text-dark" v-for="i in ingredients" :key="i.id">
            <h3>{{ i.name }}: {{ i.quantity }}</h3>
            <i
              class="mdi mdi-delete selectable"
              @click="deleteIngredient(i.id)"
            ></i>
            <i
              class="mdi mdi-pencil selectable"
              data-bs-toggle="modal"
              data-bs-target="#edit"
              @click="activeIngred(i.id)"
            ></i>
          </div>
          <CreateIngredient :idata="active" />
        </div>
        <div class="row bg-white text-dark rounded mt-2">
          <h2>The steps for the recipe</h2>
          <div class="col-10 text-info" v-for="s in steps" :key="s.id">
            <h3>{{ s.ordr }}: {{ s.body }}</h3>
            <i class="mdi mdi-delete selectable" @click="deleteStep(s.id)"></i>
          </div>
          <CreateStep :sdata="active" />
        </div>
      </div>
    </template>
  </Modal>
  <Modal id="edit">
    <template #title> More Details</template>
    <template #body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-10">
            {{ activei.name }}
          </div>
          <EditIngredient />
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { recipesService } from "../services/RecipesService"
import { favoritesService } from "../services/FavoritesService"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { ingredientsService } from "../services/IngredientsService"
export default {
  name: 'Home',
  setup() {
    watchEffect(async () => {
      try {
        await recipesService.getAllRecipes()
        await recipesService.getFavs()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      recipes: computed(() => AppState.recipes),
      async deleteRecipe(id) {
        try {
          if (await Pop.confirm()) {
            await recipesService.deleteRecipe(id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async getAll() {
        try {
          await recipesService.getAllRecipes()
        } catch (error) {
          logger.error(error)
        }
      },
      async getFavs() {
        try {
          await favoritesService.getFavs()
        } catch (error) {
          logger.error(error)

        }
      },
      async activeIngred(id) {
        try {
          await ingredientsService.getOne(id)
        } catch (error) {
          logger.error(error)
        }
      },
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps),
      active: computed(() => AppState.activeRecipe),
      activei: computed(() => AppState.activeIngredient)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
