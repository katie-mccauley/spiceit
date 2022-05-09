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
    @submit.prevent="createBoard"
  >
    <h2>Create Vault</h2>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Name: </label>
      <input
        v-model="editable.name"
        required
        maxlength="9"
        minlength="3"
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Name....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Description: </label>
      <input
        v-model="editable.description"
        required
        maxlength="50"
        minlength="3"
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Description....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Image: </label>
      <input
        v-model="editable.img"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Cover Image....."
      />
    </div>

    <div class="col-12 d-flex justify-content-end">
      <button class="btn btn-primary">create</button>
    </div>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import { Modal } from "bootstrap"
import { boardsService } from "../services/BoardsService"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createBoard() {
        try {
          Modal.getOrCreateInstance(document.getElementById('create-board')).hide()
          await boardsService.createBoard(editable.value)
          editable.value = {}
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>