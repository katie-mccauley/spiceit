<template>
  <div class="col-10 text-info">
    <h2>The ingredients for this recipe</h2>
    <div class="row">
      <div class="col-6" v-for="i in ingredients" :key="i.id">
        <h3>
          {{ i.name }}: Will need
          {{ i.quantity }}
        </h3>
        <i
          class="mdi mdi-delete selectable"
          @click="deleteIngredient(i.id)"
        ></i>
        <i
          class="mdi mdi-pencil selectable"
          @click="closeModal"
          data-bs-toggle="modal"
          data-bs-target="#editI"
        ></i>
      </div>
    </div>
    <CreateIngredient :idata="ingredients" />
    <!-- <form @submit.prevent="createIngredient">
      <div class="input-group mb-3">
        <input
          v-model="editable.name"
          required
          type="text"
          class="form-control"
          placeholder="Add Name"
          aria-label="Recipient's username"
        />
        <input
          v-model="editable.quantity"
          required
          type="text"
          class="form-control"
          placeholder="Add Quantity"
          aria-label="Recipient's username"
        />

        <button
          class="btn btn-outline-secondary"
          type="button"
          id="button-addon2"
        >
          Create
        </button>
      </div>
    </form> -->
  </div>
  <Modal id="editI">
    <template #title>
      <div class="container-fluid">
        <div class="row">
          <div class="col-4 bg-white">
            <h1>hshshshshshssh</h1>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { ingredientsService } from "../services/IngredientsService"
import { watchEffect } from "@vue/runtime-core"
import { Modal } from "bootstrap"
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
    // watchEffect(async () => {
    //   try {
    //     await ingredientsService.getAllIngredients(props.ingredientData.id);

    //   } catch (error) {
    //     logger.error(error)
    //   }
    // })
    return {
      editable,
      ingredients: computed(() => AppState.ingredients),
      async deleteIngredient(id) {
        try {
          await ingredientsService.deleteIngredient(id)
        } catch (error) {
          logger.error(error)
          Pop.toast('error')
        }
      },
      closeModal() {
        try {
          Modal.getOrCreateInstance(document.getElementById('moredetails' + props.ingredientData.id)).hide()
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