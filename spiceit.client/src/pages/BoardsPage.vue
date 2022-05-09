<template>
  <div class="container-fluid mt-4">
    <div
      class="
        row
        justify-content-md-between justify-content-around
        mb-5
        textcolor
      "
    >
      <div class="col-md-5 col-7 text-dark">
        <h1>{{ activeBoard.name }}</h1>
        <h3>Description: {{ activeBoard.description }}</h3>

        <h3>Pins: {{ pins.length }}</h3>
      </div>
      <div v-if="activeBoard.creatorId == account.id" class="col-md-2 col-3">
        <button class="btn btn-danger" @click="deleteBoard()">
          <h6 class="text-bold p-0 m-0">Delete Board</h6>
        </button>
      </div>
    </div>
    <div class="row">
      <div class="col-md-3" v-for="p in pins" :key="p.id">
        <Recipe :recipe="p" />
      </div>
    </div>
  </div>
  <RecipeDetails />
</template>


<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { pinsService } from "../services/PinsService"
import { useRoute, useRouter } from "vue-router"
import { AppState } from "../AppState"
import { boardsService } from "../services/BoardsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    watchEffect(async () => {
      await pinsService.getPins(route.params.id)
      await boardsService.setActive(route.params.id)
    })
    return {
      pins: computed(() => AppState.pins),
      activeBoard: computed(() => AppState.activeBoard),
      account: computed(() => AppState.account),
      async deleteBoard() {
        try {
          if (await Pop.confirm()) {
            router.push({ name: 'Home' })
            await boardsService.deleteBoard(route.params.id)
          }
        } catch (error) {
          logger.error(error)
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
* {
  font-family: Sahitya;
}
</style>