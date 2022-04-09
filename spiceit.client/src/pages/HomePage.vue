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
        <button class="btn-info btn">ALL</button>
        <button class="btn-info btn">Favorites</button>
        <button class="btn-info btn">My Posts</button>
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
          <h2>
            <i
              class="mdi mdi-plus"
              data-bs-toggle="modal"
              data-bs-target="createIngredient"
            ></i>
          </h2>
        </div>

        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
  <Modal id="createIngredient">
    <template #title> create an ingredient</template>
  </Modal>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { recipesService } from "../services/RecipesService"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAllRecipes()
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
      }
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
