package com.example.publicator.dto;

import jakarta.validation.constraints.Size;
import lombok.Data;

@Data
public class NoteRequestTo {
    int id;
    int tweetId;
    @Size(min = 2, max = 2048)
    String content;
}
