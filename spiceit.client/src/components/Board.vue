<template>
  <div
    class="card selectable border border-3 rounded bg-white"
    @click="goToBoard(board)"
  >
    <img
      class="img-fluid cropped rounded"
      :src="board.img"
      alt="board picture"
    />
    <div
      class="
        card-img-overlay
        d-flex
        flex-column
        justify-content-end
        backgroundfix
        pb-md-2 pb-0
      "
    >
      <div class="row justify-content-md-around">
        <div class="col-12">
          <h2
            data-bs-toggle="modal"
            data-bs-target="#more-details"
            @click="activeBoard(board.id)"
            class="text-light"
          >
            {{ board.name }}
          </h2>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from "vue-router"
import { boardsService } from "../services/BoardsService"
import { logger } from "../utils/Logger"
export default {
  props: {
    board: {
      type: Object,
      required: true
    }
  },
  setup() {
    const router = useRouter()
    return {
      async goToBoard(board) {
        const id = board.id
        router.push({ name: 'Boards', params: { id } })
      },
      async activeBoard(id) {
        try {
          await boardsService.setActive(id)
        } catch (error) {
          logger.error(error)
        }
      }
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

.backgroundfix {
  background: linear-gradient(
    180.45deg,
    rgba(0, 0, 0, 0) 67.72%,
    rgba(0, 0, 0, 0.53) 99.61%
  );
}
</style>