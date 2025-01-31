package ru.bsuir.dto.request;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import lombok.Data;


@Data
public class PostRequestTo {
    private Long id;

    @NotNull
    private Long storyId;

    @NotBlank
    @Size(min = 4, max = 2048)
    private String content;

    private String country = "Belarus";
}