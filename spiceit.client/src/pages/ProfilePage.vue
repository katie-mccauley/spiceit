<template>
  <div class="container-fluid">
    <div class="row m-3 ms-0">
      <div class="col-md-1 col-3">
        <div class="card">
          <img :src="profile.picture" class="img-fluid" alt="" />
        </div>
      </div>
      <div class="col-md-5 col-7">
        <h1>{{ profile.name }}</h1>
        <h3>Recipes: {{ recipes.length }}</h3>
        <h3>Boards: {{ boards.length }}</h3>
      </div>
    </div>
    <div class="row mt-3">
      <h2>
        Boards:
        <i
          v-if="account.id == profile.id"
          data-bs-toggle="modal"
          data-bs-target="#create-board"
          class="mdi mdi-plus text-primary selectable"
          title="Create Board"
        >
        </i>
      </h2>
      <div class="col-md-3" v-for="b in boards" :key="b.id">
        <Board :board="b" />
      </div>
    </div>
    <div class="row mt-3">
      <h2>Recipes:</h2>
      <div class="col-md-3" v-for="r in recipes" :key="r.id">
        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
  <ModalForm id="create-board">
    <template #title>Create Board</template>
    <template #body>
      <CreateBoard />
    </template>
  </ModalForm>
  <RecipeDetails />
</template>


<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { boardsService } from "../services/BoardsService"
import { recipesService } from "../services/RecipesService"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      try {
        await boardsService.getUserBoards(route.params.id)
        await recipesService.getUserRecipes(route.params.id)
        await recipesService.getUsers(route.params.id)
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      profile: computed(() => AppState.profile),
      recipes: computed(() => AppState.recipesUser),
      boards: computed(() => AppState.boardsUser),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
* {
  font-family: Sahitya;
}
</style>