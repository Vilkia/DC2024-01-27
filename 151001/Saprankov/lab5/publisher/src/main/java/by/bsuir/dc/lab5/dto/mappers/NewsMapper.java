package by.bsuir.dc.lab5.dto.mappers;

import by.bsuir.dc.lab5.dto.*;
import by.bsuir.dc.lab5.entities.News;
import org.mapstruct.Mapper;
import org.mapstruct.factory.Mappers;

@Mapper(componentModel = "spring")
public interface NewsMapper {
    NewsMapper instance = Mappers.getMapper(NewsMapper.class);
    News convertFromDTO(NewsRequestTo dto);
    News convertRequestToDTO(NewsRequestTo dto);
    NewsResponseTo convertToDTO(News news);
}
