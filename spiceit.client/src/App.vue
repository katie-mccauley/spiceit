<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <div class="row justify-content-end me-3 mb-4">
    <div class="col-1 m-2">
      <button
        data-bs-toggle="modal"
        data-bs-target="#create-recipe"
        class="btn btn-primary rounded-pill p-2 px-3 rounded shadow"
      >
        <h2 class="p-0 m-0">+</h2>
      </button>
    </div>
  </div>

  <!-- <footer>
    <div class="bg-dark text-light text-center p-4">
      Made with 💖 by Katie McCauley
    </div>
  </footer> -->
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.round {
  border-radius: 50%;
  background-color: rgb(47, 206, 255);
}
</style>
