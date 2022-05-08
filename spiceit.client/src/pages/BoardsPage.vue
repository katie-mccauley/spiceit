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
      <!-- <div v-if="activeBoard.creatorId == account.id" class="col-md-2 col-3">
        <button class="btn btn-danger" @click="deleteVault()">
          <h6 class="text-bold p-0 m-0">Delete Vault</h6>
        </button>
      </div> -->
    </div>
    <div class="row">
      <div class="col-md-3" v-for="p in pins" :key="p.id">
        <Recipe :recipe="p" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { pinsService } from "../services/PinsService"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      await pinsService.getPins(route.params.id)
    })
    return {
      pins: computed(() => AppState.pins),
      activeBoard: computed(() => AppState.activeBoard)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>